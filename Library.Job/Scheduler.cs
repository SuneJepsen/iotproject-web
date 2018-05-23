using DataLayer.Crypto;
using Library.Job.Task.Abstract;
using Library.Job.Task.Concrete;

namespace Library.Job
{
    public class Scheduler:MasterTemplate
    {
        public override void ModelData1()
        {
            //ITask doInferenceBetweenDoorAndFloorData = new InferenceLogicNaive(floorMeasurements, doorMeasurements);
            ITask doInferenceBetweenDoorAndFloorData = new InferenceLogicExperimented(floorMeasurements, doorMeasurements);
            doInferenceBetweenDoorAndFloorData.Work();
            inferredMeasurements = doInferenceBetweenDoorAndFloorData.Done();

            ITask cryptoSensitiveData = new CryptoSensitiveData(inferredMeasurements, new RijndaelManaged());
            cryptoSensitiveData.Work();
            inferredMeasurements = cryptoSensitiveData.Done();
        }

        public override void ModelData2()
        {
            throw new System.NotImplementedException();
        }

        public override void ModelData3()
        {
            throw new System.NotImplementedException();
        }
    }
}
