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
            var rawDataFloorRepo = new FirebaseDb(string.Format(FirebaseConnectionString.RawDataFloor,string.Empty));
            var rawDataDoorRepo = new FirebaseDb(string.Format(FirebaseConnectionString.RawDataDoor, string.Empty));
            var copyDataFloorRepo = new FirebaseDb(string.Format(FirebaseConnectionString.CopyDataFloor, string.Empty));
            var copyDataDoorRepo = new FirebaseDb(string.Format(FirebaseConnectionString.CopyDataDoor, string.Empty));
            var inferredDataRepo = new FirebaseDb(string.Format(FirebaseConnectionString.InferredData, string.Empty));
            rawDataFloorRepo.DeleteAll();
            rawDataDoorRepo.DeleteAll();
            copyDataDoorRepo.DeleteAll();
            copyDataFloorRepo.DeleteAll();
            inferredDataRepo.DeleteAll();
        }
    }
}
