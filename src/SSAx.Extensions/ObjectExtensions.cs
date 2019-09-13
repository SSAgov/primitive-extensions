using System;

namespace SSAx.Extensions
{
    /// <summary>
    /// Provides static methods that aid in performing object related operations.
    /// </summary>
    public static class ObjectExtensions
    {
        #region NullToZero (Nz) functions

        /// <summary>
        /// NullToZero Function for integer. Always returns an integer regardless of the state of the object.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="valueIfNull"></param>
        /// <returns>A value if null, otherwise, it's the output of the int.TryParse method.</returns>
        public static int NzInt(this object o, int valueIfNull = 0)
        {
            if (o == null) return valueIfNull;
            try
            {
                valueIfNull = Convert.ToInt32(o);
            }
            catch (Exception)
            { }
            return valueIfNull;
        }

        /// <summary>
        /// NullToZero method for a Long. This method will always return a Long regardless of the state of the object. 
        /// </summary>
        /// <param name="o"></param>
        /// <param name="valueIfNull"></param>
        /// <returns>>A value if null, otherwise, it's the output of long value</returns>
        public static long NzLong(this object o, long valueIfNull = 0)
        {
            if (o == null) return valueIfNull;
            try
            {
                string s = o.ToString();
                long.TryParse(s, out valueIfNull);
            }
            catch (Exception)
            { }
            return valueIfNull;
        }

        /// <summary>
        /// NullToZero method for a Decimal. This method will always return a Decimal regardless of the state of the object. 
        /// </summary>
        /// <param name="o"></param>
        /// <param name="ValueIfNull"></param>
        /// <returns>A value if null, otherwise, it's the output of decimal value</returns>
        public static decimal NzDecimal(this object o, decimal ValueIfNull = 0)
        {
            if (o == null && o == DBNull.Value) return ValueIfNull;
            try
            {
                string s = o.ToString();
                decimal.TryParse(s, out ValueIfNull);
            }
            catch (Exception)
            {
            }
            return ValueIfNull;
        }

        /// <summary>
        /// NullToZero funciton for a string. This method will always return a string regardless of the state of the object.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="ValueIfNull"></param>
        /// <returns>A value if null, otherwise, it's the output of string value</returns>
        public static string NzString(this object o, string ValueIfNull = "")
        {
            if (o == null || o == DBNull.Value)
                return ValueIfNull;
            try
            {
                return o.ToString();
            }
            catch (Exception)
            {
                return ValueIfNull;
            }
        }

        /// <summary>
        /// NullToZero function for a DateTime. This method will always reutrn a DateTime regardless of the state of the object.
        /// </summary>
        /// <param name="o"></param>
        /// <returns>A value if null, otherwise, it's the output of Datetime value</returns>
        public static DateTime NzDateTime(this object o)
        {
            return o.NzDateTime(new DateTime());
        }

        /// <summary>
        /// NullToZero function for a DateTime. This method will always return a DateTime regardless of the state of the object.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="dateIfNull"></param>
        /// <returns>Return a DateTime </returns>
        public static DateTime NzDateTime(this object o, DateTime dateIfNull)
        {
            if (o == null)
                return dateIfNull;

            int year = o.NzInt();
            if (year > 0 && year < DateTime.MaxValue.Year)
                return new DateTime(year, 1, 1);

            try
            {
                string s = o.ToString();
                DateTime.TryParse(s, out dateIfNull);
            }
            catch (Exception)
            {
            }
            return dateIfNull;
        }

        #endregion

        /// <summary>
        /// convert datetime to null
        /// </summary>
        /// <param name="o"></param>
        /// <returns>return a DateTime </returns>
        public static DateTime? ToNullableDateTime(this object o)
        {
            if (o == null || o == DBNull.Value)
                return null;

            return o.NzDateTime();
        }

        /// <summary>
        /// convert int to null
        /// </summary>
        /// <param name="o"></param>
        /// <returns>A value if null, otherwise, it's the output of int value</returns>
        public static int? ToNullableInt(this object o)
        {
            if (o == null || o == DBNull.Value)
                return null;

            return o.NzInt();
        }


        /// <summary>
        ///  
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <param name="trimSpacesIfApplicable"></param>
        /// <param name="dateTimeIsDateWhenMidnight"></param>
        /// <returns></returns>
        public static string EncodeAsSqlVariable(this object objectValue, bool trimSpacesIfApplicable = true, bool dateTimeIsDateWhenMidnight = true)
        {
            if (objectValue == null || objectValue == DBNull.Value)
                return "NULL";

            string encodedString = objectValue.ToString();
            switch (Type.GetTypeCode(objectValue.GetType()))
            {
                case TypeCode.Boolean:
                    bool b = (bool)objectValue;
                    return b ? "1" : "0";

                case TypeCode.String:
                    if (trimSpacesIfApplicable) encodedString = encodedString.TrimEnd();
                    encodedString = encodedString.Replace("'", "''");
                    return string.Format("'{0}'", encodedString);

                case TypeCode.DateTime:
                    DateTime dateTime = (DateTime)objectValue;
                    if (dateTime.TimeOfDay == new TimeSpan() & dateTimeIsDateWhenMidnight)
                        return dateTime.EncodeAsSqlVariable(DateTimeExtensions.DateTimePrecision.Day);
                    else
                        return dateTime.EncodeAsSqlVariable(DateTimeExtensions.DateTimePrecision.Second);

                default:
                    break;
            }
            return encodedString;
        }
    }
}
