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
    public class UnitTestInference
    {

        [TestMethod]
        public void Test_Detect_One_Person()
        {
            List<Measurement> floorData = new List<Measurement>();
            List<Measurement> doorData = new List<Measurement>();

            doorData.Add(new Measurement { StartDate = DateTime.Now.AddHours(-1), EndDate = DateTime.Now });
            floorData.Add(new Measurement { StartDate = DateTime.Now.AddMinutes(-30), EndDate = DateTime.Now.AddMinutes(-15) });

            ITask job = new DoInferenceBetweenDoorAndFloorData(floorData, doorData);
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

            doorData.Add(new Measurement { StartDate = DateTime.Now.AddHours(-2), EndDate = DateTime.Now.AddMinutes(-90) });
            floorData.Add(new Measurement { StartDate = DateTime.Now.AddMinutes(-110), EndDate = DateTime.Now.AddMinutes(-100) });

            doorData.Add(new Measurement { StartDate = DateTime.Now.AddHours(-1), EndDate = DateTime.Now });
            floorData.Add(new Measurement { StartDate = DateTime.Now.AddMinutes(-45), EndDate = DateTime.Now.AddMinutes(-30) });
            floorData.Add(new Measurement { StartDate = DateTime.Now.AddMinutes(-20), EndDate = DateTime.Now.AddMinutes(-10) });

            ITask job = new DoInferenceBetweenDoorAndFloorData(floorData, doorData);
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

            doorData.Add(new Measurement { StartDate = DateTime.Now.AddHours(-2), EndDate = DateTime.Now.AddHours(-1) });
            floorData.Add(new Measurement { StartDate = DateTime.Now.AddHours(-3), EndDate = DateTime.Now.AddMinutes(-150) });
            floorData.Add(new Measurement { StartDate = DateTime.Now.AddMinutes(-30), EndDate = DateTime.Now.AddMinutes(-15) });

            ITask job = new DoInferenceBetweenDoorAndFloorData(floorData, doorData);
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

            doorData.Add(new Measurement { StartDate = DateTime.Now.AddSeconds(-5), EndDate = DateTime.Now });
            floorData.Add(new Measurement { StartDate = DateTime.Now.AddSeconds(-7), EndDate = DateTime.Now.AddSeconds(-4) });
            floorData.Add(new Measurement { StartDate = DateTime.Now.AddSeconds(-1), EndDate = DateTime.Now.AddSeconds(2) });

            ITask job = new DoInferenceBetweenDoorAndFloorData(floorData, doorData);
            job.Work();

            Thread.Sleep(100);

            var inferenceList = job.Done();

            Assert.AreEqual(1, inferenceList.Count);
            Assert.AreEqual(2, inferenceList[0].Count);
        }
    }
}
