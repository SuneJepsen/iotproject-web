using System;
using System.Collections.Generic;
using DataLayer.Domain;
using DataLayer.Repository;
using DataLayer.Repository.Abstract;
using DataLayer.Repository.Concrete;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataLayer.Test
{
    [TestClass]
    public class UnitTestFirebaseRawDataFloor
    {

        private IRepository _repo;

        public UnitTestFirebaseRawDataFloor()
        {
            this._repo = new FirebaseDb(string.Format(FirebaseConnectionString.RawDataFloor,DateTime.Now.ToString("dd-MM-yyyy")));
        }

        [TestMethod]
        public void Test_Insert_Into_Raw_data_floor()
        {
            List<Measurement> measurements = new List<Measurement>();
            Measurement measurement = null;
            var startDate = DateTime.Now;
            var endDate = startDate;
            for (int i = 0; i < 10; i++)
            {
                startDate = startDate.AddMinutes(2);
                endDate = startDate.AddMinutes(2);
                measurement = new Measurement();
                measurement.StartDate = startDate;
                measurement.EndDate = endDate;
                measurements.Add(measurement);
            }

            _repo.Save(measurements);
        }
    }
}
