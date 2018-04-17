using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Domain
{
    public class Measurement
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int Id { get; set; }
        public string Count { get; set; }
    }
}
