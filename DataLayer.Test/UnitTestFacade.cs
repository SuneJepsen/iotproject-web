using System;
using DataLayer.Facade;
using DataLayer.Helper.HandshakeHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataLayer.Test
{
    [TestClass]
    public class UnitTestFacade
    {
        [TestMethod]
        public void TestMethod1()
        {
            //IFacade facade = new FacadeData(new HandShakeHelperSaveToFile(@"..\..\..\DataLayer\Settings\handshake.json"));
            IFacade facade = new FacadeData(new HandShakeHelperSaveToFB());

            var doorMeasurements = facade.GetAllRawDataDoorAsMeasurement();
            var floorMeasurements = facade.GetAllRawDataFloorAsMeasurement();
        }
    }
}
