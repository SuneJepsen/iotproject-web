using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Domain
{
    public class Measurement
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public DateTime? EpocToDatetime { get; set; }

        public long Epoc { get; set; }

        public Guid Id { get; set; }
        public string Count { get; set; }
        public string Title { get; set; }
    }
}
