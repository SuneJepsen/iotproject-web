//using System;
//using DataLayer.Domain;
//using DataLayer.Facade;
//using DataLayer.Helper.HandshakeHelper;
//using DataLayer.Repository;
//using DataLayer.Repository.Concrete;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace DataLayer.Test
//{
//    [TestClass]
//    public class UnitTestFacade
//    {
//        [TestMethod]
//        public void TestMethod1()
//        {

//            IFacade facade = new FacadeData(new HandShakeHelperSaveToFB());

//            var allRawSensorData  = facade.GetAllRawDataAsMeasurement();

//            facade.SaveCopyMeasurements(allRawSensorData);


//        }
//    }
//}
