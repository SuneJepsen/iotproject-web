using System;
using System.Collections.Generic;
using DataLayer.Domain;
using DataLayer.Helper.DateHelper;
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
        private IRepository<MeasurementRaw> _repo;

        public UnitTestFirebaseRawDataDoor()
        {
            this._repo = new FirebaseDb<MeasurementRaw>(string.Format(FirebaseConnectionString.RawDataDoor,string.Empty));
        }
        [TestMethod]
        public void Test_Insert_Into_Raw_data_door()
        {

            List<MeasurementRaw> measurements = new List<MeasurementRaw>();
            MeasurementRaw measurement = null;
       
            for (int i = 0; i < 2; i++)
            {
                var startDate = DateTime.Now;
                var endDate = startDate;
                Random r1 = new Random();
                Random r2 = new Random();
                int rInt1 = r1.Next(3, 6); //for int
                int rInt2 = r2.Next(2, 4);
                startDate = startDate.AddSeconds(rInt1);
                endDate = startDate.AddSeconds(rInt2);
                measurement = new MeasurementRaw();
                measurement.StartDate = startDate.Second;
                measurement.EndDate = endDate.Second;
                measurement.Time = new DateHelper().ConvertToEpoch(startDate);
                measurements.Add(measurement);
            }

           _repo.Save(measurements);
        }
        [TestMethod]
        public void Test_Delete_all_Raw_data_door()
        {
           // _repo.DeleteAll();
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


        [TestMethod]
        public void Test_Datetime()
        {
            var date = DateTime.Now.ToString("dd-MM-yyyy");
        }
    }
}
