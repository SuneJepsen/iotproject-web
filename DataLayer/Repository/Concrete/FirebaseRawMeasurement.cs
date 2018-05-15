using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using DataLayer.Domain;
using DataLayer.Repository.Abstract;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DataLayer.Repository.Concrete
{
    public class FirebaseRawMeasurement<T> : IRepository<T> where T : IMeasurement
    {
        private string _path;

        public FirebaseRawMeasurement(string path)
        {
            this._path = path;
        }
        public List<T> GetAll()
        {
            var measurements = new List<T>();
            var request = (HttpWebRequest)WebRequest.Create(_path);
            request.ContentType = "application/json: charset=utf-8";
            var reponse = request.GetResponse() as HttpWebResponse;
            if (reponse == null) return measurements;
            using (var reponsestream = reponse.GetResponseStream())
            {
                if (reponsestream == null) return measurements;
                using (var reader = new StreamReader(reponsestream, Encoding.UTF8))
                {
                    var json = reader.ReadLine();
                    if (!string.IsNullOrEmpty(json) && json!= "null")
                    {
                        var sensorIds = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

                        foreach (var sensorId in sensorIds)
                        {
                            var sensorData = JsonConvert.DeserializeObject<dynamic>(sensorId.Value.ToString());
                            foreach (var itemDynamic in sensorData)
                            {
                                var measurement = JsonConvert.DeserializeObject<T>(((JProperty)itemDynamic).Value.ToString());
                                measurement.Type = sensorId.Key;
                                measurements.Add(measurement);
                            }
                        }
                    }
                }
            }
            return measurements;
        }

        public void Save(List<T> measurements)
        {
            foreach (var measurement in measurements)
            {
                var json = JsonConvert.SerializeObject(measurement);
                var request = WebRequest.CreateHttp(_path);
                request.Method = "POST";
                request.ContentType = "application/json";
                var buffer = Encoding.UTF8.GetBytes(json);
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
                var response = request.GetResponse();
                json = (new StreamReader(response.GetResponseStream())).ReadToEnd();
            }
            
        }
        public void DeleteAll()
        {
            var request = WebRequest.CreateHttp(_path);
            request.Method = "DELETE";
            request.ContentType = "application/json";
            var response = request.GetResponse();
        }
    }
}
