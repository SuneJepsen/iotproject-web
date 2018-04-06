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
            var rawDataFloorRepo = new FirebaseDb(FirebaseConnectionString.RawDataFloor);
            var rawDataDoorRepo = new FirebaseDb(FirebaseConnectionString.RawDataDoor);
            var copyDataFloorRepo = new FirebaseDb(FirebaseConnectionString.CopyDataFloor);
            var copyDataDoorRepo = new FirebaseDb(FirebaseConnectionString.CopyDataDoor);

            var measurements1 = rawDataFloorRepo.GetAll();
            copyDataFloorRepo.Save(measurements1);
            rawDataFloorRepo.DeleteAll();

            var measurements2 = rawDataDoorRepo.GetAll();
            copyDataDoorRepo.Save(measurements2);
            rawDataDoorRepo.DeleteAll();

        }
    }
}
