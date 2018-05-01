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
    public class FirebaseDb<T>: IRepository<T>
    {
        private string _path;

        public FirebaseDb(string path)
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
                        var data = JsonConvert.DeserializeObject<dynamic>(json);
                        //var firebaseLookup = JsonConvert.DeserializeObject<Dictionary<string, T>>(json);
                        foreach (var itemDynamic in data)
                        {
                            measurements.Add(JsonConvert.DeserializeObject<T>(((JProperty)itemDynamic).Value.ToString()));
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
