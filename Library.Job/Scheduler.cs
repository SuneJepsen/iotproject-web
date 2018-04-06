using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Job.Task.Abstract;
using Library.Job.Task.Concrete;

namespace Library.Job
{
    public class Scheduler:MasterTemplate
    {
        public override void ModelData()
        {
            ITask doInferenceBetweenDoorAndFloorData =
                new DoInferenceBetweenDoorAndFloorData(floorMeasurements, doorMeasurements);
            doInferenceBetweenDoorAndFloorData.Work();
            inferredMeasurements = doInferenceBetweenDoorAndFloorData.Done();
        }
    }
}
