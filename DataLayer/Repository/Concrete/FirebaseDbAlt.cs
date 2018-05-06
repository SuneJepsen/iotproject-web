using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using DataLayer.Domain;
using DataLayer.Repository.Abstract;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DataLayer.Repository.Concrete
{
    public class FirebaseDbAlt<T>: IRepository<T> where T : IMeasurement
    {
        private string _path;
        private string _type;
        
        public FirebaseDbAlt(string path, string type)
        {
            _path = path;
            _type = type;
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
                        foreach (var itemDynamic in data)
                        {
                            var  measurement = JsonConvert.DeserializeObject<T>(((JProperty) itemDynamic).Value.ToString());
                            measurement.Type = _type;
                            measurements.Add(measurement);
                        }
                    }
                }
            }
            return measurements;
        }

        public void Save(List<T> measurements)
        {
        }
        public void DeleteAll()
        {
        }
    }
}
