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
            this.rawDataFloorRepo = new FirebaseDb(FirebaseConnectionString.RawDataFloor);
            this.rawDataDoorRepo = new FirebaseDb(FirebaseConnectionString.RawDataDoor);
            this.copyDataFloorRepo = new FirebaseDb(FirebaseConnectionString.CopyDataFloor);
            this.copyDataDoorRepo = new FirebaseDb(FirebaseConnectionString.CopyDataDoor);
            this.inferredDataRepo = new FirebaseDb(FirebaseConnectionString.InferredData);
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
