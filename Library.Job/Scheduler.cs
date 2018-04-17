using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Crypto;
using Library.Job.Task.Abstract;
using Library.Job.Task.Concrete;

namespace Library.Job
{
    public class Scheduler:MasterTemplate
    {
        public override void ModelData()
        {
            ITask doInferenceBetweenDoorAndFloorData = new DoInferenceBetweenDoorAndFloorData(floorMeasurements, doorMeasurements);
            doInferenceBetweenDoorAndFloorData.Work();
            inferredMeasurements = doInferenceBetweenDoorAndFloorData.Done();
            
            ITask cryptoSensitiveData = new CryptoSensitiveData(inferredMeasurements, new RijndaelManaged());
            cryptoSensitiveData.Work();
            inferredMeasurements = cryptoSensitiveData.Done();





        }
    }
}
