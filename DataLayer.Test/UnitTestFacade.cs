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
            IFacade facade = new FacadeData(new HandShakeHelper(@"..\..\..\DataLayer\Settings\handshake.json"));

            var doorMeasurements = facade.GetAllRawDataDoorAsMeasurement();
            var floorMeasurements = facade.GetAllRawDataFloorAsMeasurement();
        }
    }
}
