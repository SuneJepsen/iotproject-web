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
        private const int buffer = 3;

        public DoInferenceBetweenDoorAndFloorData(List<Measurement> floorMeasurements, List<Measurement> doorMeasurements)
        {
            _floorMeasurements = floorMeasurements;
            _doorMeasurements = doorMeasurements;
            _inferredMeasurements = new List<Measurement>();
        }
        public void Work()
        {
            var doorStart = DateTime.Now;
            var doorEnd = DateTime.Now;
            var floorIndex = 0;
            var incDec = 1;
            foreach (var m in _doorMeasurements)
            {
                doorStart = m.StartDate.AddSeconds(-buffer);
                doorEnd = m.EndDate.AddSeconds(buffer);

                if (_floorMeasurements[floorIndex].StartDate > m.StartDate)
                {
                    incDec = 1;
                }
                else
                {
                    incDec = -1;
                }

                var count = 0;
                while (floorIndex < _floorMeasurements.Count && _floorMeasurements[floorIndex].EndDate <= doorEnd)
                {
                    if (_floorMeasurements[floorIndex].StartDate >= doorStart)
                    {
                        count += incDec;
                    }

                    floorIndex++;
                }
                _inferredMeasurements.Add(new Measurement { StartDate = m.StartDate, EndDate = m.EndDate, Count = count });
            }
            
        }

        public List<Measurement> Done()
        {
            return _inferredMeasurements;
        }
    }
}
