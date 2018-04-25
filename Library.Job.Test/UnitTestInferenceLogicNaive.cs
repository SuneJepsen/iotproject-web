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
    public class UnitTestInferenceLogicNaive
    {

        [TestMethod]
        public void Test_Detect_One_Person()
        {
            List<Measurement> floorData = new List<Measurement>();
            List<Measurement> doorData = new List<Measurement>();

            doorData.Add(new Measurement { StartDate = DateTime.Now.AddSeconds(-5), EndDate = DateTime.Now });
            floorData.Add(new Measurement { StartDate = DateTime.Now.AddSeconds(-3), EndDate = DateTime.Now.AddSeconds(-1) });

            ITask job = new InferenceLogicNaive(floorData, doorData);
            job.Work();

            Thread.Sleep(100);

            var inferenceList = job.Done();

            Assert.AreEqual(1, inferenceList.Count);
            Assert.AreEqual(1, inferenceList[0].Count);
        }

        [TestMethod]
        public void Test_Detect_Three_Persons()
        {
            List<Measurement> floorData = new List<Measurement>();
            List<Measurement> doorData = new List<Measurement>();

            doorData.Add(new Measurement { StartDate = DateTime.Now.AddSeconds(-10), EndDate = DateTime.Now.AddSeconds(-7) });
            floorData.Add(new Measurement { StartDate = DateTime.Now.AddSeconds(-9), EndDate = DateTime.Now.AddSeconds(-8) });

            doorData.Add(new Measurement { StartDate = DateTime.Now.AddSeconds(-5), EndDate = DateTime.Now });
            floorData.Add(new Measurement { StartDate = DateTime.Now.AddSeconds(-4), EndDate = DateTime.Now.AddSeconds(-3) });
            floorData.Add(new Measurement { StartDate = DateTime.Now.AddSeconds(-2), EndDate = DateTime.Now.AddSeconds(-1) });

            ITask job = new InferenceLogicNaive(floorData, doorData);
            job.Work();

            Thread.Sleep(100);

            var inferenceList = job.Done();

            Assert.AreEqual(2, inferenceList.Count);
            Assert.AreEqual(1, inferenceList[0].Count);
            Assert.AreEqual(2, inferenceList[1].Count);
        }

        [TestMethod]
        public void Test_Detect_No_Person()
        {
            List<Measurement> floorData = new List<Measurement>();
            List<Measurement> doorData = new List<Measurement>();

            doorData.Add(new Measurement { StartDate = DateTime.Now.AddSeconds(-10), EndDate = DateTime.Now.AddSeconds(-7) });
            floorData.Add(new Measurement { StartDate = DateTime.Now.AddSeconds(-2), EndDate = DateTime.Now.AddSeconds(-1) });
            floorData.Add(new Measurement { StartDate = DateTime.Now.AddSeconds(-18), EndDate = DateTime.Now.AddSeconds(-15) });

            ITask job = new InferenceLogicNaive(floorData, doorData);
            job.Work();

            Thread.Sleep(100);

            var inferenceList = job.Done();

            Assert.AreEqual(1, inferenceList.Count);
            Assert.AreEqual(0, inferenceList[0].Count);
        }

        [TestMethod]
        public void Test_Detect_One_Person_In_Buffer()
        {
            List<Measurement> floorData = new List<Measurement>();
            List<Measurement> doorData = new List<Measurement>();

            doorData.Add(new Measurement { StartDate = DateTime.Now.AddSeconds(-30), EndDate = DateTime.Now.AddSeconds(-25) });
            floorData.Add(new Measurement { StartDate = DateTime.Now.AddSeconds(-33), EndDate = DateTime.Now.AddSeconds(-31) });

            doorData.Add(new Measurement { StartDate = DateTime.Now.AddSeconds(-20), EndDate = DateTime.Now.AddSeconds(-15) });
            floorData.Add(new Measurement { StartDate = DateTime.Now.AddSeconds(-18), EndDate = DateTime.Now.AddSeconds(-16) });

            doorData.Add(new Measurement { StartDate = DateTime.Now.AddSeconds(-10), EndDate = DateTime.Now });
            floorData.Add(new Measurement { StartDate = DateTime.Now.AddSeconds(-12), EndDate = DateTime.Now.AddSeconds(-8) });
            floorData.Add(new Measurement { StartDate = DateTime.Now.AddSeconds(-6), EndDate = DateTime.Now.AddSeconds(-4) });

            ITask job = new InferenceLogicNaive(floorData, doorData);
            job.Work();

            Thread.Sleep(100);

            var inferenceList = job.Done();

            Assert.AreEqual(3, inferenceList.Count);
            Assert.AreEqual(-1, inferenceList[0].Count);
            Assert.AreEqual(1, inferenceList[1].Count);
            Assert.AreEqual(-2, inferenceList[2].Count);
        }
    }
}
