using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Helper.DateHelper
{
    public class DateHelper: IDateHelper
    {
        private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public DateTime ConvertFromEpoch(long time)
        {
            var realTime = epoch.AddSeconds(time);
            return realTime;
        }

        public DateTime GetDateTime(int sec, DateTime handshake)
        {
            var dateTime = handshake.AddSeconds(sec);
            return dateTime;
        }

        public long ConvertToEpoch( DateTime date)
        {
            return Convert.ToInt64((date - epoch).TotalSeconds);
        }
    }
}
