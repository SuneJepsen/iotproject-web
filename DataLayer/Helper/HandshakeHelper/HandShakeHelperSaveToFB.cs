using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.Domain;
using DataLayer.Repository;
using DataLayer.Repository.Abstract;
using DataLayer.Repository.Concrete;

namespace DataLayer.Helper.HandshakeHelper
{
    public class HandShakeHelperSaveToFB : IHandShakeHelper
    {

        private IRepository<Handshake> dataHandshakeRepo;

        private DateHelper.DateHelper _dateHelper;

        public HandShakeHelperSaveToFB()
        {
            dataHandshakeRepo = new FirebaseDb<Handshake>(string.Format(FirebaseConnectionString.HandShakeData, string.Empty));
            _dateHelper = new DateHelper.DateHelper();
        }

        private List<Handshake> GetHandshakeEpochList()
        {
            List<Handshake> handshakeEpochs= dataHandshakeRepo.GetAll();
            return handshakeEpochs;
        }
        public DateTime? GetHandShakeFloor()
        {
            List<Handshake> handshakeEpochs = GetHandshakeEpochList();
            DateTime? floorHandshake = null;
            if (handshakeEpochs == null) return null;
            Handshake handshakeEpoch = handshakeEpochs.OrderByDescending(x => x.CreatedDate).FirstOrDefault(); 
            if (handshakeEpoch != null && handshakeEpoch.Promixitmity.HasValue)
            {
                floorHandshake = _dateHelper.ConvertFromEpoch(handshakeEpoch.Promixitmity.Value);
            }
            return floorHandshake;
        }

        public DateTime? GetHandShakeDoor()
        {
            List<Handshake> handshakeEpochs = GetHandshakeEpochList();
            DateTime? doorHandshake = null;
            if (handshakeEpochs == null) return null;
            Handshake handshakeEpoch = handshakeEpochs.OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            if (handshakeEpoch != null && handshakeEpoch.Accelometer.HasValue)
            {
                doorHandshake = _dateHelper.ConvertFromEpoch(handshakeEpoch.Accelometer.Value);
            }
            return doorHandshake;
        }

        public void SaveHandshake(Handshake handshake)
        {
            dataHandshakeRepo.Save(new List<Handshake>(){handshake});
        }

    }
}
