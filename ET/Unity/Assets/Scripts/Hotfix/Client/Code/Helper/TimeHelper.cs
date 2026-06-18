using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public static class TimeHelper
    {
        /// <summary>
        /// long转DateTime
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static DateTime ConvertLongToDateTime(long d)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(d + "0000");
            TimeSpan toNow = new TimeSpan(lTime);
            DateTime dtResult = dtStart.Add(toNow);
            return dtResult;
        }

        public static string ConvertLongToString(long d) 
        {
            return string.Format("{0:yyyy/MM/dd HH:mm:ss}", ConvertLongToDateTime(d));
        }

        public static string LongTimeToHour(long scends)
        {
            long hours = scends / 3600000;
            long mins = (scends % 3600000) / 60000;
            // Make sure you use the appropriate decimal separator
            return string.Format("{0:D2}:{1:D2}:{2:D2}", hours, mins, scends % 60000 / 1000);
        }

        public static string ScendsToHour(long scends)
        {
            long hours = scends / 3600;
            long mins = (scends % 3600) / 60;
            // Make sure you use the appropriate decimal separator
            return string.Format("{0:D2}:{1:D2}:{2:D2}", hours, mins, scends % 60);
        }

        public static string LongTimeToDay(long d)
        {
            DateTime endDateTime=ConvertLongToDateTime(d);
            return ((int)(endDateTime - DateTime.Now).TotalDays).ToString();
        }

        public static TimeSpan LongTimeToTimeSpan(long d)
        {
            DateTime endDateTime = ConvertLongToDateTime(d);
            return endDateTime - DateTime.Now;
        }
    }
}
