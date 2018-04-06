using System;
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
            var rawDataFloorRepo = new FirebaseDb(FirebaseConnectionString.RawDataFloor);
            var rawDataDoorRepo = new FirebaseDb(FirebaseConnectionString.RawDataDoor);
            var copyDataFloorRepo = new FirebaseDb(FirebaseConnectionString.CopyDataFloor);
            var copyDataDoorRepo = new FirebaseDb(FirebaseConnectionString.CopyDataDoor);
            var inferredDataRepo = new FirebaseDb(FirebaseConnectionString.InferredData);
            rawDataFloorRepo.DeleteAll();
            rawDataDoorRepo.DeleteAll();
            copyDataDoorRepo.DeleteAll();
            copyDataFloorRepo.DeleteAll();
            inferredDataRepo.DeleteAll();
        }
    }
}
