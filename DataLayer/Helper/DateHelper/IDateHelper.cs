using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Helper.DateHelper
{
    public interface IDateHelper
    {
        DateTime ConvertFromEpoch(long time);
        DateTime GetDateTime(int sec, DateTime handshake);
        long ConvertToEpoch(DateTime date);
    }
}
