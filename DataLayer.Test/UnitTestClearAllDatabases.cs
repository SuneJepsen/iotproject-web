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
            var rawDataRepo = new FirebaseDb<MeasurementRaw>(string.Format(FirebaseConnectionString.RawData,string.Empty, string.Empty));
            var copyDataRepo = new FirebaseDb<Measurement>(string.Format(FirebaseConnectionString.CopyData, string.Empty));
            var handshakeRepo = new FirebaseDb<Handshake>(string.Format(FirebaseConnectionString.HandShakeData, string.Empty));
            var inferredDataRepo = new FirebaseDb<Measurement>(string.Format(FirebaseConnectionString.InferredData, string.Empty));
            //rawDataRepo.DeleteAll();
            copyDataRepo.DeleteAll();
            //handshakeRepo.DeleteAll();
            inferredDataRepo.DeleteAll();
        }
    }
}
