using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Library.Job.Test
{
    [TestClass]
    public class UnitTestMasterTemplate
    {
        [TestMethod]
        public void Test_Run_MasterTemplate()
        {
            MasterTemplate m = new Scheduler();
            m.Run();
        }
    }
}
