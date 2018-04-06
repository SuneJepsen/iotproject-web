using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Domain;
using Library.Job.Task.Abstract;

namespace Library.Job.Task.Concrete
{
    public class DoInferenceBetweenDoorAndFloorData: ITask
    {
        private List<Measurement> _floorMeasurements;
        private List<Measurement> _doorMeasurements;
        private List<Measurement> _inferredMeasurements;

        public DoInferenceBetweenDoorAndFloorData(List<Measurement> floorMeasurements, List<Measurement> doorMeasurements)
        {
            _floorMeasurements = floorMeasurements;
            _doorMeasurements = doorMeasurements;
            _inferredMeasurements = new List<Measurement>();
        }
        public void Work()
        {
            _inferredMeasurements.AddRange(_floorMeasurements);
            _inferredMeasurements.AddRange(_doorMeasurements);
        }

        public List<Measurement> Done()
        {
            return _inferredMeasurements;
        }
    }
}
