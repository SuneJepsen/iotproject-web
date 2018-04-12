﻿using System;
using System.Collections.Generic;
using DataLayer.Domain;
using DataLayer.Repository;
using DataLayer.Repository.Abstract;
using DataLayer.Repository.Concrete;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DataLayer.Test
{
    [TestClass]
    public class UnitTestFirebaseRawDataDoor
    {
        private IRepository _repo;

        public UnitTestFirebaseRawDataDoor()
        {
            this._repo = new FirebaseDb(FirebaseConnectionString.RawDataDoor);
        }
        [TestMethod]
        public void Test_Insert_Into_Raw_data_door()
        {

            List<Measurement> measurements = new List<Measurement>();
            Measurement measurement = null;

            for (int i = 0; i < 10; i++)
            {
                measurement = new Measurement();
                measurement.StartDate = DateTime.Now;
                measurement.EndDate = DateTime.Now.AddMinutes(2);
                measurements.Add(measurement);
            }

            _repo.Save(measurements);
        }
        [TestMethod]
        public void Test_Delete_all_Raw_data_door()
        {
            _repo.DeleteAll();
        }

        [TestMethod]
        public void Test_Read_all_Raw_data_door()
        {
            var measurements = _repo.GetAll();
        }


        [TestMethod]
        public void Test_Convert_from_json_objects()
        {
            // https://stackoverflow.com/questions/24940591/deserialize-response-from-firebase
            string json =
                "{\"-L9RkZB_0_6B-60l2uJz\":{\"EndDate\":\"2018-04-06T23:01:57.3965276+02:00\",\"StartDate\":\"2018-04-06T22:59:57.3945275+02:00\"},\"-L9RkZLHDOWa1Xus_Gac\":{\"EndDate\":\"2018-04-06T23:01:57.3965276+02:00\",\"StartDate\":\"2018-04-06T22:59:57.3965276+02:00\"},\"-L9RkZUtU3_c3Tu11Nxv\":{\"EndDate\":\"2018-04-06T23:01:57.3965276+02:00\",\"StartDate\":\"2018-04-06T22:59:57.3965276+02:00\"},\"-L9RkZdRWJFlnGgj-oOt\":{\"EndDate\":\"2018-04-06T23:01:57.3965276+02:00\",\"StartDate\":\"2018-04-06T22:59:57.3965276+02:00\"},\"-L9RkZmeFPHlz4noHWLK\":{\"EndDate\":\"2018-04-06T23:01:57.3965276+02:00\",\"StartDate\":\"2018-04-06T22:59:57.3965276+02:00\"},\"-L9RkZrVPQfAknZ7lM1W\":{\"EndDate\":\"2018-04-06T23:01:57.3965276+02:00\",\"StartDate\":\"2018-04-06T22:59:57.3965276+02:00\"},\"-L9RkZwgMNTXRVozyZ6h\":{\"EndDate\":\"2018-04-06T23:01:57.3965276+02:00\",\"StartDate\":\"2018-04-06T22:59:57.3965276+02:00\"},\"-L9Rk_0kGo6KxKgu8Qv_\":{\"EndDate\":\"2018-04-06T23:01:57.3965276+02:00\",\"StartDate\":\"2018-04-06T22:59:57.3965276+02:00\"},\"-L9Rk_5c-8ibeJEy3RUY\":{\"EndDate\":\"2018-04-06T23:01:57.3965276+02:00\",\"StartDate\":\"2018-04-06T22:59:57.3965276+02:00\"},\"-L9Rk_ALeRw0BMwRh5H9\":{\"EndDate\":\"2018-04-06T23:01:57.3965276+02:00\",\"StartDate\":\"2018-04-06T22:59:57.3965276+02:00\"}}";

            var data = JsonConvert.DeserializeObject<dynamic>(json);
            var measurements = new List<Measurement>();
            foreach (var itemDynamic in data)
            {
                measurements.Add(JsonConvert.DeserializeObject<Measurement>(((JProperty)itemDynamic).Value.ToString()));
            }
            foreach (var measurement in measurements)
            {
                if (measurement != null)
                {
                    var startDate = measurement.StartDate;
                }
            }
        }
    }
}