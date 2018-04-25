using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Domain;
using Library.Job.Task.Abstract;

namespace Library.Job.Task.Concrete
{
    public class InferenceLogicExperimented: ITask
    {
        private List<Measurement> _floorMeasurements;
        private List<Measurement> _doorMeasurements;
        private List<Measurement> _inferredMeasurements;
        private const int in_buffer = 4;
        private const int out_buffer = 2;

        public InferenceLogicExperimented(List<Measurement> floorMeasurements, List<Measurement> doorMeasurements)
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
                if (floorIndex >= _floorMeasurements.Count)
                {
                    return;
                }
                doorStart = m.StartDate;
                doorEnd = m.EndDate;
                if (_floorMeasurements[floorIndex].StartDate >= doorStart.AddSeconds(in_buffer))
                {
                    incDec = 1;
                } else
                {
                    incDec = -1;
                }

                var count = 0;
                while (floorIndex < _floorMeasurements.Count && _floorMeasurements[floorIndex].EndDate <= doorEnd)
                {
                    if (_floorMeasurements[floorIndex].StartDate >= doorStart.AddSeconds(-out_buffer))
                    {
                        count += incDec;
                    }

                    floorIndex++;
                }
                _inferredMeasurements.Add(new Measurement { StartDate = m.StartDate, EndDate = m.EndDate, Count = count.ToString() });
            }
            
        }

        public List<Measurement> Done()
        {
            return _inferredMeasurements;
        }
    }
}
