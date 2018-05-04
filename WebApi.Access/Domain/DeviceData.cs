using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer.Domain;

namespace WebApi.Access.Domain
{
    public class DeviceData
    {
        public string  XLabel{ get; set; }
        public string  YLabel{ get; set; }
        public string  Title{ get; set; }

        public string Type { get; set; }
        public List<Measurement> Measurements{ get; set; }

        public DeviceData()
        {
            Measurements = new List<Measurement>();
        }
    }
}