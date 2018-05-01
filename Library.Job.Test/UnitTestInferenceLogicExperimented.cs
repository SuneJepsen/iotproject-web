using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library.Job.Task.Abstract;
using Library.Job.Task.Concrete;
using System.Collections.Generic;
using DataLayer.Domain;
using System.Threading;

namespace Library.Job.Test
{
    [TestClass]
    public class UnitTestInferenceLogicExperimented
    {

        [TestMethod]
        public void Test_Detect_One_Person()
        {
            List<Measurement> floorData = new List<Measurement>();
            List<Measurement> doorData = new List<Measurement>();

            doorData.Add(new Measurement { StartDate = DateTime.Now.AddSeconds(-10), EndDate = DateTime.Now });
            floorData.Add(new Measurement { StartDate = DateTime.Now.AddSeconds(-6), EndDate = DateTime.Now.AddSeconds(-3) });

            ITask job = new InferenceLogicExperimented(floorData, doorData);
            job.Work();

            Thread.Sleep(100);

            var inferenceList = job.Done();

            Assert.AreEqual(1, inferenceList.Count);
            Assert.AreEqual(1, Int32.Parse(inferenceList[0].Count));
        }

        [TestMethod]
        public void Test_Detect_Three_Persons()
        {
            List<Measurement> floorData = new List<Measurement>();
            List<Measurement> doorData = new List<Measurement>();

            doorData.Add(new Measurement { StartDate = DateTime.Now.AddSeconds(-20), EndDate = DateTime.Now.AddSeconds(-12) });
            floorData.Add(new Measurement { StartDate = DateTime.Now.AddSeconds(-15), EndDate = DateTime.Now.AddSeconds(-13) });

            doorData.Add(new Measurement { StartDate = DateTime.Now.AddSeconds(-10), EndDate = DateTime.Now });
            floorData.Add(new Measurement { StartDate = DateTime.Now.AddSeconds(-6), EndDate = DateTime.Now.AddSeconds(-4) });
            floorData.Add(new Measurement { StartDate = DateTime.Now.AddSeconds(-3), EndDate = DateTime.Now.AddSeconds(-1) });

            ITask job = new InferenceLogicExperimented(floorData, doorData);
            job.Work();

            Thread.Sleep(100);

            var inferenceList = job.Done();

            Assert.AreEqual(2, inferenceList.Count);
            Assert.AreEqual(1, Int32.Parse(inferenceList[0].Count));
            Assert.AreEqual(2, Int32.Parse(inferenceList[1].Count));
        }

        [TestMethod]
        public void Test_Detect_No_Person()
        {
            List<Measurement> floorData = new List<Measurement>();
            List<Measurement> doorData = new List<Measurement>();

            doorData.Add(new Measurement { StartDate = DateTime.Now.AddSeconds(-10), EndDate = DateTime.Now.AddSeconds(-7) });
            floorData.Add(new Measurement { StartDate = DateTime.Now.AddSeconds(-2), EndDate = DateTime.Now.AddSeconds(-1) });
            floorData.Add(new Measurement { StartDate = DateTime.Now.AddSeconds(-18), EndDate = DateTime.Now.AddSeconds(-15) });

            ITask job = new InferenceLogicExperimented(floorData, doorData);
            job.Work();

            Thread.Sleep(100);

            var inferenceList = job.Done();

            Assert.AreEqual(1, inferenceList.Count);
            Assert.AreEqual(0, Int32.Parse(inferenceList[0].Count));
        }

        [TestMethod]
        public void Test_Detect_One_Person_In_And_Out()
        {
            List<Measurement> floorData = new List<Measurement>();
            List<Measurement> doorData = new List<Measurement>();

            doorData.Add(new Measurement { StartDate = DateTime.Now.AddSeconds(-30), EndDate = DateTime.Now.AddSeconds(-25) });
            floorData.Add(new Measurement { StartDate = DateTime.Now.AddSeconds(-31), EndDate = DateTime.Now.AddSeconds(-28) });

            doorData.Add(new Measurement { StartDate = DateTime.Now.AddSeconds(-20), EndDate = DateTime.Now.AddSeconds(-14) });
            floorData.Add(new Measurement { StartDate = DateTime.Now.AddSeconds(-16), EndDate = DateTime.Now.AddSeconds(-14) });

            doorData.Add(new Measurement { StartDate = DateTime.Now.AddSeconds(-10), EndDate = DateTime.Now });
            floorData.Add(new Measurement { StartDate = DateTime.Now.AddSeconds(-8), EndDate = DateTime.Now.AddSeconds(-6) });
            floorData.Add(new Measurement { StartDate = DateTime.Now.AddSeconds(-4), EndDate = DateTime.Now.AddSeconds(-2) });

            ITask job = new InferenceLogicExperimented(floorData, doorData);
            job.Work();

            Thread.Sleep(100);

            var inferenceList = job.Done();

            Assert.AreEqual(3, inferenceList.Count);
            Assert.AreEqual(-1, Int32.Parse(inferenceList[0].Count));
            Assert.AreEqual(1, Int32.Parse(inferenceList[1].Count));
            Assert.AreEqual(-2, Int32.Parse(inferenceList[2].Count));
        }
    }
}
