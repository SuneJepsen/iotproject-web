using System;
using DataLayer.Repository;
using DataLayer.Repository.Abstract;
using DataLayer.Repository.Concrete;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataLayer.Test
{
    [TestClass]
    public class UnitTestMoveRawDataToCopyData
    {
        [TestMethod]
        public void Test_Move_RawData_To_CopyData()
        {
            var rawDataFloorRepo = new FirebaseDb(string.Format(FirebaseConnectionString.RawDataFloor,DateTime.Now.ToString("dd-MM-yyyy")));
            var rawDataDoorRepo = new FirebaseDb(string.Format(FirebaseConnectionString.RawDataDoor, DateTime.Now.ToString("dd-MM-yyyy")));
            var copyDataFloorRepo = new FirebaseDb(string.Format(FirebaseConnectionString.CopyDataFloor, DateTime.Now.ToString("dd-MM-yyyy")));
            var copyDataDoorRepo = new FirebaseDb(string.Format(FirebaseConnectionString.CopyDataDoor, DateTime.Now.ToString("dd-MM-yyyy")));

            var measurements1 = rawDataFloorRepo.GetAll();
            copyDataFloorRepo.Save(measurements1);
            rawDataFloorRepo.DeleteAll();

            var measurements2 = rawDataDoorRepo.GetAll();
            copyDataDoorRepo.Save(measurements2);
            rawDataDoorRepo.DeleteAll();

        }
    }
}
