using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using DataLayer.Crypto;
using DataLayer.Domain;
using DataLayer.Helper.DateHelper;
using DataLayer.Helper.HandshakeHelper;
using DataLayer.Repository;
using DataLayer.Repository.Abstract;
using DataLayer.Repository.Concrete;

namespace DataLayer.Facade
{
    public class FacadeData : IFacade
    {
        private IRepository<MeasurementRaw> rawDataRepo;
        private IRepository<Measurement> copyDataRepo;
        private List<Handshake> handshakes = new List<Handshake>();
        private IRepository<Measurement> inferredDataRepo;
        private IDateHelper _dateHelper;
        private IHandShakeHelper _handShakeHelper;
        private RijndaelManaged _cryptographyTool;

        public FacadeData(IHandShakeHelper handShakeHelper)
        {
            _handShakeHelper = handShakeHelper;
            _dateHelper = new DateHelper();
            copyDataRepo = new FirebaseCopyMeasurement<Measurement>(string.Format(FirebaseConnectionString.CopyData, string.Empty));
            inferredDataRepo = new FirebaseDb<Measurement>(string.Format(FirebaseConnectionString.InferredData, DateTime.Now.ToString("dd-MM-yyyy")));
            _cryptographyTool = new RijndaelManaged();
        }
        private List<Measurement> ConvertToMeasurement(List<MeasurementRaw> rawData, DateTime handShake)
        {
            List<Measurement> measurements = new List<Measurement>();
            Measurement measurement = null;
            foreach (MeasurementRaw measurementRaw in rawData)
            {
                if (measurementRaw.StartDate != null && measurementRaw.EndDate!= null)
                {
                    measurement = new Measurement();
                    measurement.Id = Guid.NewGuid();
                    measurement.StartDate = _dateHelper.GetDateTime(measurementRaw.StartDate.Value, handShake);
                    measurement.EndDate = _dateHelper.GetDateTime(measurementRaw.EndDate.Value, handShake);
                    measurement.Epoc = measurementRaw.Time;
                    measurement.EpocToDatetime = _dateHelper.ConvertFromEpoch(measurementRaw.Time);
                    measurement.Type = measurementRaw.Type;
                    measurement.Title= measurementRaw.Title;
                    measurements.Add(measurement);
                }

            }
            return measurements;
        }


        public List<Measurement> GetAllRawDataAsMeasurement()
        {

            rawDataRepo = new FirebaseRawMeasurement<MeasurementRaw>(string.Format(FirebaseConnectionString.RawData, string.Empty, string.Empty));
            var allRawSensorData = rawDataRepo.GetAll().ToList();
            IEnumerable<MeasurementRaw> filteredList = allRawSensorData.GroupBy(sensor => sensor.Type).Select(group => group.First());
            foreach (var sensor in filteredList)
            {
                var candidateForHandshake = allRawSensorData.Where(x => x.Type == sensor.Type).FirstOrDefault(x => x.StartDate == 0 && x.EndDate == 0);
                if (candidateForHandshake != null)
                {
                    var handshake = new Handshake()
                    {
                        Id = candidateForHandshake.Type,
                        Epoch = candidateForHandshake.Time,
                        EpochToDatetime = _dateHelper.ConvertFromEpoch(candidateForHandshake.Time),
                        CreatedDate = DateTime.Now
                    };
                    _handShakeHelper.SaveHandshake(handshake);
                    handshakes.Add(handshake);
                }
            }
            if (!handshakes.Any())
            {
                handshakes = _handShakeHelper.GetHandShakes();
            }


            if (!handshakes.Any()) throw new Exception("no handshake available");
            List<Measurement> measurements = new List<Measurement>();
            foreach (var handshake in handshakes)
            {
                var sensorData = allRawSensorData.Where(x => x.Type == handshake.Id).ToList();
                if (handshake.EpochToDatetime != null)
                {
                    var convertedMeasurements = ConvertToMeasurement(sensorData, handshake.EpochToDatetime.Value);
                    measurements.AddRange(convertedMeasurements);
                }
            }
            return measurements;
        }

        public void SaveCopyMeasurements(List<Measurement> measurements)
        {
            var allCopySensorData = copyDataRepo.GetAll();
            List<Measurement> filteredCopySensorData = allCopySensorData.GroupBy(sensor => sensor.Type).Select(group => group.First()).ToList();
            if (filteredCopySensorData.Any())
            {
                foreach (var sensor in filteredCopySensorData)
                {
                    var measurementsToSave = measurements.Where(x => x.Type == sensor.Type).ToList();

                    if (measurementsToSave.Any())
                    {
                        var copyDataRepoConcrete =new FirebaseDb<Measurement>(string.Format(FirebaseConnectionString.CopyData,sensor.Type));
                        copyDataRepoConcrete.Save(measurementsToSave);
                    }
                }
            }
            else
            {
                List<Measurement> filteredMeasurements = measurements.GroupBy(sensor => sensor.Type).Select(group => group.First()).ToList();
                foreach (var sensor in filteredMeasurements)
                {
                    var measurementsToSave = measurements.Where(x => x.Type == sensor.Type).ToList();

                    if (measurementsToSave.Any())
                    {
                        var copyDataRepoConcrete = new FirebaseDb<Measurement>(string.Format(FirebaseConnectionString.CopyData, sensor.Type));
                        copyDataRepoConcrete.Save(measurementsToSave);
                    }
                }
            }
        }

        public List<Measurement> GetAllCopyData()
        {
            return copyDataRepo.GetAll();
        }
        public void SaveInferredMeasurements(List<Measurement> measurements)
        {
            foreach (var measurement in measurements)
            {
                measurement.Count = _cryptographyTool.Encrypt(measurement.Count, CryptoConstants.passPhrase);
            }

            inferredDataRepo.Save(measurements);
        }

        public void DeleteRawDataFloor()
        {
            //rawDataFloorRepo.DeleteAll();
        }

        public void DeleteRawDataDoor()
        {
           // rawDataDoorRepo.DeleteAll();
        }

        public List<Measurement> GetAllInferredData()
        {
            return inferredDataRepo.GetAll();
        }
    }
}
