using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.Crypto;
using DataLayer.Domain;
using DataLayer.Helper.DateHelper;
using DataLayer.Helper.HandshakeHelper;
using DataLayer.Repository;
using DataLayer.Repository.Concrete;

namespace DataLayer.Facade
{
    public class FacadeData: IFacade
    {
        private FirebaseDb<MeasurementRaw> rawDataFloorRepo;
        private FirebaseDb<MeasurementRaw> rawDataDoorRepo;
        private FirebaseDb<Measurement> copyDataFloorRepo;
        private FirebaseDb<Measurement> copyDataDoorRepo;
        private FirebaseDb<Measurement> inferredDataRepo;
        private IDateHelper _dateHelper;
        private DateTime? _handshakeFloor;
        private DateTime? _handshakeDoor;
        private IHandShakeHelper _handShakeHelper;
        private RijndaelManaged _cryptographyTool;

        public FacadeData()
        {
            rawDataFloorRepo = new FirebaseDb<MeasurementRaw>(string.Format(FirebaseConnectionString.RawDataFloor, string.Empty));
            rawDataDoorRepo = new FirebaseDb<MeasurementRaw>(string.Format(FirebaseConnectionString.RawDataDoor, string.Empty));
            copyDataFloorRepo = new FirebaseDb<Measurement>(string.Format(FirebaseConnectionString.CopyDataFloor, DateTime.Now.ToString("dd-MM-yyyy")));
            copyDataDoorRepo = new FirebaseDb<Measurement>(string.Format(FirebaseConnectionString.CopyDataDoor, DateTime.Now.ToString("dd-MM-yyyy")));
            inferredDataRepo = new FirebaseDb<Measurement>(string.Format(FirebaseConnectionString.InferredData, DateTime.Now.ToString("dd-MM-yyyy")));
            _cryptographyTool = new RijndaelManaged();
            _dateHelper = new DateHelper();
            _handShakeHelper = new HandShakeHelper();
            _handshakeFloor = _handShakeHelper.GetHandShakeFloor();
            _handshakeDoor = _handShakeHelper.GetHandShakeDoor();
            if (!_handshakeDoor.HasValue || !_handshakeFloor.HasValue)
            {
                var handshakeFloor = rawDataFloorRepo.GetAll().Where(x=>x.StartDate==0 && x.EndDate==0).SingleOrDefault();
                var handshakeDoor= rawDataDoorRepo.GetAll().Where(x => x.StartDate == 0 && x.EndDate == 0).SingleOrDefault();
                if (handshakeDoor != null && handshakeFloor != null)
                {
                    var handshake = new Handshake()
                    {
                        Accelometer = handshakeDoor.Time,
                        Promixitmity = handshakeFloor.Time,
                        CreatedDate = DateTime.Now
                    };
                    _handShakeHelper.SaveHandshake(handshake);
                    _handshakeFloor = _handShakeHelper.GetHandShakeFloor();
                    _handshakeDoor = _handShakeHelper.GetHandShakeDoor();
                }
            }
        }
        private List<Measurement> ConvertToMeasurement(List<MeasurementRaw> rawData, DateTime handShake)
        {
            List<Measurement> measurements = new List<Measurement>();
            Measurement measurement = null;
            foreach (MeasurementRaw measurementRaw in rawData)
            {
                measurement = new Measurement();
                measurement.Id = measurementRaw.Id;
                measurement.StartDate = _dateHelper.GetDateTime(measurementRaw.StartDate, handShake);
                measurement.EndDate = _dateHelper.GetDateTime(measurementRaw.EndDate, handShake);
                measurement.Epoc = measurementRaw.Time;
                measurement.EpocToDatetime = _dateHelper.ConvertFromEpoch(measurementRaw.Time);
                measurements.Add(measurement);
            }
            return measurements;
        }
        public List<Measurement> GetAllRawDataFloorAsMeasurement()
        {
            if(!_handshakeFloor.HasValue) throw  new Exception("no handshakefloor value");
            var rawData = rawDataFloorRepo.GetAll();
            List<Measurement> measurements = ConvertToMeasurement(rawData, _handshakeFloor.Value);
            return measurements;
        }

        public List<Measurement> GetAllRawDataDoorAsMeasurement()
        {
            if (!_handshakeDoor.HasValue) throw new Exception("no handshakeDoor value");
            var rawData = rawDataDoorRepo.GetAll();
            List<Measurement> measurements = ConvertToMeasurement(rawData, _handshakeDoor.Value);
            return measurements;
        }

        public void SaveCopyFloorMeasurements(List<Measurement> measurements)
        {
            copyDataFloorRepo.Save(measurements);
        }

        public void SaveCopyDoorMeasurements(List<Measurement> measurements)
        {
            copyDataDoorRepo.Save(measurements);
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
            throw new NotImplementedException();
        }

        public void DeleteRawDataDoor()
        {
            throw new NotImplementedException();
        }

        public List<Measurement> GetAllInferredData()
        {
            return inferredDataRepo.GetAll();
        }


        //public List<Measurement> GetDataForCurrentDay()
        //{
        //    var encryptedMeasurements = _repository.GetAll();
        //    var decryptedMeasurements = new List<Measurement>();
        //    Measurement decryptedMeasurement = null;
        //    foreach (var encryptedMeasurement in encryptedMeasurements)
        //    {
        //        decryptedMeasurement = new Measurement();
        //        decryptedMeasurement.Id = encryptedMeasurement.Id;
        //        decryptedMeasurement.StartDate = encryptedMeasurement.StartDate;
        //        decryptedMeasurement.EndDate = encryptedMeasurement.EndDate;
        //        decryptedMeasurement.Count = _cryptographyTool.Decrypt(encryptedMeasurement.Count, CryptoConstants.passPhrase);
        //        decryptedMeasurements.Add(decryptedMeasurement);
        //    }
        //    return decryptedMeasurements;
        //}

    }
}
