using System;
using System.Linq;

namespace SSAx.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Returns date value in a string format at a specified precision
        /// </summary>
        /// <param name="datetime"></param>
        /// <param name="precision"></param>
        /// <returns></returns>
        public static string EncodeAsSqlVariable(this DateTime datetime, DateTimePrecision precision)
        {
            string encodedString = "";
            string format = "";
            switch (precision)
            {
                
                case DateTimePrecision.Year:
                    format = "yyyy-MM-dd";
                    datetime = new DateTime(datetime.Year, 1, 1);

                    break;
                case DateTimePrecision.Month:
                    format = "yyyy-MM-dd";
                    datetime = new DateTime(datetime.Year, datetime.Month, 1);
                    break;

                case DateTimePrecision.Day:
                    format = "yyyy-MM-dd";
                    break;

                case DateTimePrecision.Hour:
                    format = "yyyy-MM-dd HH:mm:ss";
                    datetime = new DateTime(datetime.Year, datetime.Month, datetime.Day,datetime.Hour,0,0);
                    break;

                case DateTimePrecision.Minute:
                    format = "yyyy-MM-dd HH:mm:ss";
                    datetime = new DateTime(datetime.Year, datetime.Month, datetime.Day, datetime.Hour, datetime.Minute, 0);
                    break;

                case DateTimePrecision.Second:
                    format = "yyyy-MM-dd HH:mm:ss";
                    break;

                case DateTimePrecision.Millisecond:
                    format = "yyyy-MM-dd HH:mm:ss.fff";
                    break;

                default:
                    break;
            }
            encodedString = string.Format($"'{datetime.ToString(format)}'");
            return encodedString;
        }

        public enum DateTimePrecision{
            Year,
            Month,
            Day,
            Hour,
            Minute,
            Second,
            Millisecond
        }
    }
}