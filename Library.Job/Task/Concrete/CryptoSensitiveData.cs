using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Crypto;
using DataLayer.Domain;
using Library.Job.Task.Abstract;

namespace Library.Job.Task.Concrete
{
    public class CryptoSensitiveData:ITask
    {
        private List<Measurement> _inferredMeasurements;
        private ICryptography _cryptographyTool;

        public CryptoSensitiveData(List<Measurement> inferredMeasurements, ICryptography cryptographyTool)
        {
            _inferredMeasurements = inferredMeasurements;
            _cryptographyTool = cryptographyTool;
        }
        public void Work()
        {
            foreach (var measurement in _inferredMeasurements)
            {
                measurement.Count = _cryptographyTool.Encrypt(measurement.Count, CryptoConstants.passPhrase);
            }
        }

        public List<Measurement> Done()
        {
            return _inferredMeasurements;
        }
    }
}
