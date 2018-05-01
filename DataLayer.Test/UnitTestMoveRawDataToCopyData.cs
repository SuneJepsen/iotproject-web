using System;
using DataLayer.Domain;
using DataLayer.Facade;
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
            IFacade facade = new FacadeData(DateTime.Now, DateTime.Now);
            var floorMeasurements = facade.GetAllRawDataFloorAsMeasurement();
            facade.SaveCopyFloorMeasurements(floorMeasurements);
            //rawDataFloorRepo.DeleteAll();
            var doorMeasurements = facade.GetAllRawDataDoorAsMeasurement();
            facade.SaveCopyDoorMeasurements(doorMeasurements);
            //rawDataDoorRepo.DeleteAll();
        }
    }
}
