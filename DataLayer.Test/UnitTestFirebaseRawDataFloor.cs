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
            this._repo = new FirebaseDb(FirebaseConnectionString.RawDataFloor);
        }

        [TestMethod]
        public void Test_Insert_Into_Raw_data_floor()
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
    }
}
