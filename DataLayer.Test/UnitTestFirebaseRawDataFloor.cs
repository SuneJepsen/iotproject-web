using System;
using System.Collections.Generic;
using DataLayer.Domain;
using DataLayer.Helper.DateHelper;
using DataLayer.Repository;
using DataLayer.Repository.Abstract;
using DataLayer.Repository.Concrete;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataLayer.Test
{
    [TestClass]
    public class UnitTestFirebaseRawDataFloor
    {

        private IRepository<MeasurementRaw> _repo;

        public UnitTestFirebaseRawDataFloor()
        {
            this._repo = new FirebaseDb<MeasurementRaw>(string.Format(FirebaseConnectionString.RawDataFloor,string.Empty));
        }

        [TestMethod]
        public void Test_Insert_Into_Raw_data_floor()
        {
            List<MeasurementRaw> measurements = new List<MeasurementRaw>();
            MeasurementRaw measurement = null;

            for (int i = 0; i < 1; i++)
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
    }
}
