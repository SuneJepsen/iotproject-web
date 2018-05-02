﻿using System;
using DataLayer.Facade;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataLayer.Test
{
    [TestClass]
    public class UnitTestFacade
    {
        [TestMethod]
        public void TestMethod1()
        {
            IFacade facade = new FacadeData();

            var doorMeasurements = facade.GetAllRawDataDoorAsMeasurement();
            var floorMeasurements = facade.GetAllRawDataFloorAsMeasurement();
        }
    }
}
