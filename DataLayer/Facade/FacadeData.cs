using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.Crypto;
using DataLayer.Domain;
using DataLayer.Helper.DateHelper;
using DataLayer.Helper.HandshakeHelper;
using DataLayer.Repository;
using DataLayer.Repository.Abstract;
using DataLayer.Repository.Concrete;

namespace DataLayer.Facade
{
    public class FacadeData: IFacade
    {
        private IRepository<MeasurementRaw> rawDataFloorRepo;
        private IRepository<MeasurementRaw> rawDataDoorRepo;
        private IRepository<Measurement> copyDataFloorRepo;
        private IRepository<Measurement> copyDataDoorRepo;
        private IRepository<Measurement> inferredDataRepo;
        private IDateHelper _dateHelper;
        private DateTime? _handshakeFloor;
        private DateTime? _handshakeDoor;
        private IHandShakeHelper _handShakeHelper;
        private RijndaelManaged _cryptographyTool;

        public FacadeData(IHandShakeHelper handShakeHelper)
        {
            rawDataFloorRepo = new FirebaseDbAlt<MeasurementRaw>(string.Format(FirebaseConnectionString.RawDataFloor, FirebaseConnectionString.RawDataFloorNodeId, string.Empty), FirebaseConnectionString.RawDataFloorNodeId);
            rawDataDoorRepo = new FirebaseDbAlt<MeasurementRaw>(string.Format(FirebaseConnectionString.RawDataDoor, FirebaseConnectionString.RawDataDoorNodeId, string.Empty), FirebaseConnectionString.RawDataDoorNodeId);
            copyDataFloorRepo = new FirebaseDb<Measurement>(string.Format(FirebaseConnectionString.CopyDataFloor, DateTime.Now.ToString("dd-MM-yyyy")));
            copyDataDoorRepo = new FirebaseDb<Measurement>(string.Format(FirebaseConnectionString.CopyDataDoor, DateTime.Now.ToString("dd-MM-yyyy")));
            inferredDataRepo = new FirebaseDb<Measurement>(string.Format(FirebaseConnectionString.InferredData, DateTime.Now.ToString("dd-MM-yyyy")));
            _cryptographyTool = new RijndaelManaged();
            _dateHelper = new DateHelper();
            _handShakeHelper = handShakeHelper;
            _handshakeFloor = _handShakeHelper.GetHandShakeFloor();
            _handshakeDoor = _handShakeHelper.GetHandShakeDoor();
        
            var handshakeFloor = rawDataFloorRepo.GetAll().Where(x=>x.StartDate==0 && x.EndDate==0).FirstOrDefault();
            var handshakeDoor= rawDataDoorRepo.GetAll().Where(x => x.StartDate == 0 && x.EndDate == 0).FirstOrDefault();
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
            rawDataFloorRepo.DeleteAll();
        }

        public void DeleteRawDataDoor()
        {
            rawDataDoorRepo.DeleteAll();
        }

        public List<Measurement> GetAllInferredData()
        {
            return inferredDataRepo.GetAll();
        }
        public List<Measurement> GetAllCopyDataDoor()
        {
            return copyDataDoorRepo.GetAll();
        }
        public List<Measurement> GetAllCopyDataFloor()
        {
            return copyDataFloorRepo.GetAll();
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
