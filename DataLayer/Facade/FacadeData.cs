using System;
using System.Collections.Generic;
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
        private DateTime _handshakeFloor;
        private DateTime _handshakeDoor;
        private IHandShakeHelper _handShakeHelper;

        public FacadeData(DateTime? handshakeFloor, DateTime? handshakeDoor)
        {
            rawDataFloorRepo = new FirebaseDb<MeasurementRaw>(string.Format(FirebaseConnectionString.RawDataFloor, string.Empty));
            rawDataDoorRepo = new FirebaseDb<MeasurementRaw>(string.Format(FirebaseConnectionString.RawDataDoor, string.Empty));
            copyDataFloorRepo = new FirebaseDb<Measurement>(string.Format(FirebaseConnectionString.CopyDataFloor, DateTime.Now.ToString("dd-MM-yyyy")));
            copyDataDoorRepo = new FirebaseDb<Measurement>(string.Format(FirebaseConnectionString.CopyDataDoor, DateTime.Now.ToString("dd-MM-yyyy")));
            inferredDataRepo = new FirebaseDb<Measurement>(string.Format(FirebaseConnectionString.InferredData, DateTime.Now.ToString("dd-MM-yyyy")));
            _dateHelper = new DateHelper();
            _handshakeFloor = handshakeFloor ?? DateTime.Now;
            _handshakeDoor = handshakeDoor ?? DateTime.Now;
            _handShakeHelper = new HandShakeHelper();

            //ToDo: Handshake logic
            // Check if handshakeFloor exist in file
            // If it does not exist then query repo
            // Save it to file
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
            var rawData = rawDataFloorRepo.GetAll();
            List<Measurement> measurements = ConvertToMeasurement(rawData, _handshakeFloor);
            return measurements;
        }

        public List<Measurement> GetAllRawDataDoorAsMeasurement()
        {
            var rawData = rawDataDoorRepo.GetAll();
            List<Measurement> measurements = ConvertToMeasurement(rawData, _handshakeDoor);
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
