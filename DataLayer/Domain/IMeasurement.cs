using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Domain
{
    public interface IMeasurement
    {
        int? StartDate { get; set; }
        int? EndDate { get; set; }

        string Id { get; set; }
        string Title { get; set; }
        long Time { get; set; }

        string Type { get; set; }

    }
}
