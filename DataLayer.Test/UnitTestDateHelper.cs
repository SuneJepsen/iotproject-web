using System;
using DataLayer.Helper.DateHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataLayer.Test
{
    [TestClass]
    public class UnitTestDateHelper
    {
        [TestMethod]
        public void Test_ConvertFromEpoch()
        {
            var dateHelper = new DateHelper();
            var reelTime = dateHelper.ConvertFromEpoch(1524747665);
        }
    }
}
