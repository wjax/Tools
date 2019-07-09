using System;
using System.Collections.Generic;
using System.Text;

namespace Tools
{
    public class TimeTools
    {
        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private const int LEAP_SECONDS = 18;

        public static long millisFromEpoch(DateTime d)
        {
            return (long)(d - UnixEpoch).TotalMilliseconds;
        }

        public static long millisFromEpochNow()
        {
            return (long)(DateTime.Now - UnixEpoch).TotalMilliseconds;
        }

        public static long microsFromEpoch(DateTime d)
        {
            return (long)(((d - UnixEpoch).TotalMilliseconds) * 1000);
        }

        public static DateTime currentDate()
        {
            return new DateTime();
        }
        public static DateTime fromMillis(long millis)
        {
            TimeSpan time = TimeSpan.FromMilliseconds(millis);
            DateTime result = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            result = result.Add(time);

            return result;

        }

        public static string fromMillisNice(long millis)
        {
            string format = "dd_MMM_HH_mm_ss";
            DateTime d = fromMillis(millis);

            string date = d.ToString(format); ;

            return date;

        }

        public static string dateAsString4FileName(DateTime d)
        {
            string format = "dd_MM_HH_mm_ss";
            string date = d.ToString(format); ;

            return date;
        }

        public static DateTime GetTimeFromGps(int weeknumber, double seconds, bool useLEAP)
        {
            DateTime datum = new DateTime(1980, 1, 6, 0, 0, 0, DateTimeKind.Utc);

            try
            {
                DateTime week = datum.AddDays(weeknumber * 7);
                DateTime time = week.AddSeconds(seconds);

                if (useLEAP)
                    return time.AddSeconds(-LEAP_SECONDS);
                else
                    return time;

            }
            catch (Exception e)
            {
                return datum;
            }
        }

        public static int CurrentLocalCoarseMillis()
        {
            return Environment.TickCount & Int32.MaxValue;
        }
    }
}
