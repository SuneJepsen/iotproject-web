using System;
using System.Collections.Generic;
using DataLayer.Domain;
using DataLayer.Repository;
using DataLayer.Repository.Concrete;

namespace Library.Job
{
    public abstract class MasterTemplate
    {
        private FirebaseDb rawDataFloorRepo;
        private FirebaseDb rawDataDoorRepo;
        private FirebaseDb copyDataFloorRepo;
        private FirebaseDb copyDataDoorRepo;
        private FirebaseDb inferredDataRepo;
        protected List<Measurement> floorMeasurements;
        protected List<Measurement> doorMeasurements;
        protected List<Measurement> inferredMeasurements;

        protected MasterTemplate()
        {
            this.rawDataFloorRepo = new FirebaseDb(string.Format(FirebaseConnectionString.RawDataFloor, DateTime.Now.ToString("dd-MM-yyyy")));
            this.rawDataDoorRepo = new FirebaseDb(string.Format(FirebaseConnectionString.RawDataDoor, DateTime.Now.ToString("dd-MM-yyyy")));
            this.copyDataFloorRepo = new FirebaseDb(string.Format(FirebaseConnectionString.CopyDataFloor, DateTime.Now.ToString("dd-MM-yyyy")));
            this.copyDataDoorRepo = new FirebaseDb(string.Format(FirebaseConnectionString.CopyDataDoor, DateTime.Now.ToString("dd-MM-yyyy")));
            this.inferredDataRepo = new FirebaseDb(string.Format(FirebaseConnectionString.InferredData, DateTime.Now.ToString("dd-MM-yyyy")));
            inferredMeasurements = new List<Measurement>();
        }

        private void Initialize()
        {
            floorMeasurements = rawDataFloorRepo.GetAll();
            doorMeasurements = rawDataDoorRepo.GetAll();
        }

        public abstract void ModelData();

        public void Run()
        {
            Initialize();
            ModelData();
            End();
        }


        private void End()
        {
            copyDataFloorRepo.Save(floorMeasurements);
            rawDataFloorRepo.DeleteAll();
            copyDataDoorRepo.Save(doorMeasurements);
            rawDataDoorRepo.DeleteAll();
            inferredDataRepo.Save(inferredMeasurements);
        }
 

    }
}
