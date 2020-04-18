using System;
using System.Collections;
using System.Collections.Generic;


namespace Danish.Covid.Utility
{
    public static class Utility
    {
        public static long UnixEpochTicks = 621355968000000000;
        public static long TicksPerMillisecond = 10000;
        public static long TicksPerSecond = TicksPerMillisecond * 1000;

        public static DateTime FromUnixTime(long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddMilliseconds(unixTime);
        }

        public static long ToUnixTime(this DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((date - epoch).TotalSeconds);
        }
        public static DateTime FromUnixTimestamp(long unixTime)
        {
            return new DateTime(UnixEpochTicks + unixTime * TicksPerSecond);
        }
    }

}
