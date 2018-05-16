using System.Collections.Generic;
using System.Linq;
using DataLayer.Domain;
using DataLayer.Facade;
using DataLayer.Helper.HandshakeHelper;

namespace Library.Job
{
    public abstract class MasterTemplate
    {
        protected List<Measurement> allRawMeasurements;
        protected List<Measurement> floorMeasurements;
        protected List<Measurement> doorMeasurements;
        protected List<Measurement> inferredMeasurements;
        protected IFacade facade;

        protected MasterTemplate()
        {
            facade = new FacadeData(new HandShakeHelperSaveToFB());
            inferredMeasurements = new List<Measurement>();
        }

        private void Initialize()
        {
            allRawMeasurements = facade.GetAllRawDataAsMeasurement();
            doorMeasurements = allRawMeasurements.Where(x => x.Title == "ACC").ToList();
            floorMeasurements = allRawMeasurements.Where(x => x.Title == "PRX").ToList();
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
            facade.SaveCopyMeasurements(allRawMeasurements);
            facade.DeleteRawData();
            facade.SaveInferredMeasurements(inferredMeasurements);
        }
 

    }
}
