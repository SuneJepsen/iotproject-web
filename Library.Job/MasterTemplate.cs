using System;
using System.Collections.Generic;
using DataLayer.Domain;
using DataLayer.Facade;
using DataLayer.Helper.HandshakeHelper;
using DataLayer.Repository;
using DataLayer.Repository.Concrete;

namespace Library.Job
{
    public abstract class MasterTemplate
    {
        protected List<Measurement> floorMeasurements;
        protected List<Measurement> doorMeasurements;
        protected List<Measurement> inferredMeasurements;
        protected IFacade facade;

        protected MasterTemplate()
        {
            facade = new FacadeData(new HandShakeHelper(@"..\..\..\DataLayer\Settings\handshake.json"));
            inferredMeasurements = new List<Measurement>();
        }

        private void Initialize()
        {
           
            floorMeasurements = facade.GetAllRawDataFloorAsMeasurement();
            doorMeasurements = facade.GetAllRawDataDoorAsMeasurement();
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
            facade.SaveCopyFloorMeasurements(floorMeasurements);
            //facade.DeleteRawDataFloor();
            facade.SaveCopyDoorMeasurements(doorMeasurements);
            //facade.DeleteRawDataDoor();
            facade.SaveInferredMeasurements(inferredMeasurements);
        }
 

    }
}
