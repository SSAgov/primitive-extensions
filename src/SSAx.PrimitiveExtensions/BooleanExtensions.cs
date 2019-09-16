using System;
using System.Linq;

namespace SSAx.PrimitiveExtensions
{
    public static class BooleanExtensions
    {
        /// <summary>
        /// This method can be called on a boolean type object
        /// It returns value associated with true and false based on boolean state
        /// </summary>
        /// <param name="b"></param>
        /// <param name="trueValue"></param>
        /// <param name="falseValue"></param>
        /// <param name="nullValue"></param>
        /// <returns></returns>
        public static string ToIndicatingStrings(this bool? b, string trueValue, string falseValue = "", string nullValue = null)
        {
            if (b == null)
                return nullValue;
            else
                return b.Value.ToIndicatingStrings(trueValue, falseValue);
        }
        public static string ToSwitch(this bool? b, string trueValue = "Y", string falseValue= "N")
        {
            return b.ToIndicatingStrings(trueValue, falseValue);
        }

        /// <summary>
        /// Converts a boolean to a string. Zero returns false, any non-zero returns true.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="trueValue"></param>
        /// <param name="falseValue"></param>
        /// <returns></returns>
        public static string ToIndicatingStrings(this bool b, string trueValue, string falseValue="")
        {
            return b ? trueValue : falseValue;
        }
        /// <summary>
        /// Converts an unsigned integer to a boolean. Zero returns false, any non-zero returns true.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static bool ToBool(this int i)
        {
            return i != 0;
        }
        /// <summary>
        /// Converts a decimal to a boolean. Zero returns false, any non-zero returns true.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static bool ToBool(this decimal d)
        {
            return d != 0;
        }

        /// <summary>
        /// Converts a string to a nullable boolean. Null, whitespace, and empty returns null.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool? ToBoolNullable(this string s)
        {
            if (s.IsNullOrEmptyString() || s == string.Empty|| string.IsNullOrWhiteSpace(s))
            {
                return null;
            }
            return s.ToBool();
        }
        
        /// <summary>
        /// Converts a string to a boolean. Zero returns false, any non-zero returns true.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool ToBool(this string s)
        {
            if (s == null)
                throw new ArgumentNullException();

            if (s.Length == 0 )
            {
                throw new Exception("Empty string not allowed");
            }
            string[] trueStrings = { "Y", "1", "-1", "TRUE", "YES", "T", "POSITIVE" };
            string[] falseStrings = { "N", "0", "FALSE", "F", "NO", "NEGATIVE" };

            if (trueStrings.Contains(s.ToUpper()))
            {
                return true;
            }
            if (falseStrings.Contains(s.ToUpper()))
            {
                return false;
            }
            char firstChar = s.ToUpper()[0];
            if (firstChar == 'Y')
            {
                return true;
            }
            if (firstChar == 'N')
            {
                return false;
            }

            if (s.IsANumber())
            {
                if (s.IsAWholeNumber())
                {
                    return s.NzInt().ToBool();
                }
                if(s.IsADecimal())
                {
                    return s.NzDecimal().ToBool();
                }
            }

            throw new Exception("No viable match");
        }
    }
}
