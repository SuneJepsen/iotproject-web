using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataLayer.Domain;
using Newtonsoft.Json;

namespace DataLayer.Helper.HandshakeHelper
{
    public class HandShakeHelperSaveToFile : IHandShakeHelper
    {
        private DateHelper.DateHelper _dateHelper;
        private string _handshakeFilePath;

        public HandShakeHelperSaveToFile(string handshakeFilePath)
        {
            _handshakeFilePath = handshakeFilePath;
            _dateHelper = new DateHelper.DateHelper();
        }

        private List<Handshake> GetHandshakeEpochList()
        {

            var filepath = AppDomain.CurrentDomain.BaseDirectory;
            //string newPath = Path.GetFullPath(Path.Combine(filepath, @"..\DataLayer\Settings\handshake.json"));
            string newPath = Path.GetFullPath(Path.Combine(filepath, _handshakeFilePath));

            List<Handshake> handshakeEpochs = new List<Handshake>();
            using (StreamReader r = new StreamReader(newPath))
            {
                string json = r.ReadToEnd();
                handshakeEpochs = JsonConvert.DeserializeObject<List<Handshake>>(json);
            }
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
            List<Handshake> handshakeEpochs = GetHandshakeEpochList();
            if(handshakeEpochs==null) handshakeEpochs = new List<Handshake>();
            //System.IO.File.WriteAllText(@"..\..\..\DataLayer\Settings\handshake.json", string.Empty);
            handshakeEpochs.Add(handshake);
            string jsonlist = JsonConvert.SerializeObject(handshakeEpochs, Formatting.Indented);
            var filepath = AppDomain.CurrentDomain.BaseDirectory;
            string newPath = Path.GetFullPath(Path.Combine(filepath, _handshakeFilePath));
            //string newPath = Path.GetFullPath(Path.Combine(filepath, @"..\DataLayer\Settings\handshake.json"));
            File.WriteAllText(newPath, jsonlist);
        }

    }
}
