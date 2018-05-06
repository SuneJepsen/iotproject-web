using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Domain
{
    public class MeasurementRaw:IMeasurement
    {
        public int? StartDate { get; set; }
        public int? EndDate { get; set; }

        public string Id { get; set; }
        public string Title { get; set; }
        public long Time { get; set; }
        public string Type { get; set; }
    }
}
