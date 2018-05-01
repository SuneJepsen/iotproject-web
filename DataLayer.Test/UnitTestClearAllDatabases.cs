using System;
using DataLayer.Domain;
using DataLayer.Repository;
using DataLayer.Repository.Concrete;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataLayer.Test
{
    [TestClass]
    public class UnitTestClearAllDatabases
    {
        [TestMethod]
        public void Test_Clear_all_databases()
        {
            //var rawDataFloorRepo = new FirebaseDb<MeasurementRaw>(string.Format(FirebaseConnectionString.RawDataFloor,string.Empty));
            //var rawDataDoorRepo = new FirebaseDb<MeasurementRaw>(string.Format(FirebaseConnectionString.RawDataDoor, string.Empty));
            var copyDataFloorRepo = new FirebaseDb<Measurement>(string.Format(FirebaseConnectionString.CopyDataFloor, string.Empty));
            var copyDataDoorRepo = new FirebaseDb<Measurement>(string.Format(FirebaseConnectionString.CopyDataDoor, string.Empty));
            var inferredDataRepo = new FirebaseDb<Measurement>(string.Format(FirebaseConnectionString.InferredData, string.Empty));
            //rawDataFloorRepo.DeleteAll();
            //rawDataDoorRepo.DeleteAll();
            copyDataDoorRepo.DeleteAll();
            copyDataFloorRepo.DeleteAll();
            inferredDataRepo.DeleteAll();
        }
    }
}
