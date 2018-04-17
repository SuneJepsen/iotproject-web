using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer.Crypto;
using DataLayer.Domain;
using DataLayer.Repository.Abstract;

namespace WebApi.Access.ServiceLayer
{
    public class InferredDataService
    {
        private ICryptography _cryptographyTool;
        private IRepository _repository;

        public InferredDataService(IRepository repository, ICryptography cryptographyTool)
        {
            _repository = repository;
            _cryptographyTool = cryptographyTool;
        }

        public List<Measurement> GetDataForCurrentDay()
        {
            var encryptedMeasurements = _repository.GetAll();
            var decryptedMeasurements = new List<Measurement>();
            Measurement decryptedMeasurement = null;
            foreach (var encryptedMeasurement in encryptedMeasurements)
            {
                decryptedMeasurement = new Measurement();
                decryptedMeasurement.Id = encryptedMeasurement.Id;
                decryptedMeasurement.StartDate = encryptedMeasurement.StartDate;
                decryptedMeasurement.EndDate = encryptedMeasurement.EndDate;
                decryptedMeasurement.Count = _cryptographyTool.Decrypt(encryptedMeasurement.Count, CryptoConstants.passPhrase);
                decryptedMeasurements.Add(decryptedMeasurement);
            }
            return decryptedMeasurements;
        }




    }
}