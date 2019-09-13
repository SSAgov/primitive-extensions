using System;

namespace SSAx.Extensions
{
    /// <summary>
    ///  Provides static methods that aid in performing numbers related operations.
    /// </summary>
    public static class NumberExtensions
    {
        #region integral parity
        /// <summary>
        /// Retruns true if the byte (8 bit unsigned integer) is odd
        /// </summary>
        /// <param name="b"></param>
        /// <returns>True if odd number</returns>
        public static bool IsOdd(this byte b)
        {
            return b % 2 != 0;
        }

        /// <summary>
        /// Retruns true if the signed byte (8 bit signed integer) is odd
        /// </summary>
        /// <param name="b"></param>
        /// <returns>True if odd number</returns>
        public static bool IsOdd(this sbyte b)
        {
            return b % 2 != 0;
        }


        /// <summary>
        /// Retruns true if the short number  (16 bit signed integer) is odd
        /// </summary>
        /// <param name="s"></param>
        /// <returns>True if odd number</returns>
        public static bool IsOdd(this short s)
        {
            return s % 2 != 0;
        }

        /// <summary>
        /// Retruns true if the ushort number (16 bit unsigned integer) is odd
        /// </summary>
        /// <param name="s"></param>
        /// <returns>True if odd number</returns>
        public static bool IsOdd(this ushort s)
        {
            return s % 2 != 0;
        }


        /// <summary>
        ///  Retruns true if the int number (32 bit signed integer) is odd
        /// </summary>
        /// <param name="i"></param>
        /// <returns>True if odd number</returns>
        public static bool IsOdd(this int i)
        {
            return i % 2 != 0;
        }

        /// <summary>
        /// Retruns true if the uint number  (32 bit unsigned integer) is odd
        /// </summary>
        /// <param name="u"></param>
        /// <returns>True if odd number</returns>
        public static bool IsOdd(this uint u)
        {
            return u % 2 != 0;
        }

        /// <summary>
        /// Retruns true if the long number (64 bit signed integer) is odd
        /// </summary>
        /// <param name="l"></param>
        /// <returns>True if odd number</returns>
        public static bool IsOdd(this long l)
        {
            return l % 2 != 0;
        }


        /// <summary>
        /// Retruns true if the ulong number (64 bit unsigned integer) is odd
        /// </summary>
        /// <param name="l"></param>
        /// <returns>True if odd number</returns>
        public static bool IsOdd(this ulong l)
        {
            return l % 2 != 0;
        }

        /// <summary>
        ///  Retruns true if the byte (8 bit unsigned integer) is even
        /// </summary>
        /// <param name="b"></param>
        /// <returns>True if odd number</returns>
        public static bool IsEven(this byte b)
        {
            return b % 2 == 0;
        }


        /// <summary>
        ///  Retruns true if the signed byte (8 bit signed integer) is even
        /// </summary>
        /// <param name="b"></param>
        /// <returns>True if odd number</returns>
        public static bool IsEven(this sbyte b)
        {
            return b % 2 == 0;
        }

        /// <summary>
        ///  Retruns true if the short number  (16 bit signed integer) is even
        /// </summary>
        /// <param name="s"></param>
        /// <returns>True if even number</returns>
        public static bool IsEven(this short s)
        {
            return s % 2 == 0;
        }

        /// <summary>
        ///  Retruns true if the ushort number (16 bit unsigned integer) is even
        /// </summary>
        /// <param name="s"></param>
        /// <returns>True if even number</returns>
        public static bool IsEven(this ushort s)
        {
            return s % 2 == 0;
        }

        /// <summary>
        /// Retruns true if the int number (32 bit signed integer) is even
        /// </summary>
        /// <param name="i"></param>
        /// <returns>True if even number</returns>
        public static bool IsEven(this int i)
        {
            return i % 2 == 0;
        }


        /// <summary>
        ///  Retruns true if the uint number (32 bit unsigned integer) is even
        /// </summary>
        /// <param name="u"></param>
        /// <returns>True if even number</returns>
        public static bool IsEven(this uint u)
        {
            return u % 2 == 0;
        }

        /// <summary>
        ///  Retruns true if the long number (64 bit signed integer) is even
        /// </summary>
        /// <param name="l"></param>
        /// <returns>True if even number</returns>
        public static bool IsEven(this long l)
        {
            return l % 2 == 0;
        }

        /// <summary>
        /// Retruns true if the ulong number (64 bit unsigned integer) is even
        /// </summary>
        /// <param name="l"></param>
        /// <returns>True if even number</returns>
        public static bool IsEven(this ulong l)
        {
            return l % 2 == 0;
        }

        /// <summary>
        /// Retruns true if the double number (double precision) is even
        /// </summary>
        /// <param name="d"></param>
        /// <returns>True if even number</returns>
        public static bool IsEven(this double d)
        {
            string s = d.ToString();
            string n = s.Right(1);
            int i= Int32.Parse(n);
            return i % 2 == 0;
        }

        /// <summary>
        /// Retruns true if the double number (double precision) is odd
        /// </summary>
        /// <param name="d"></param>
        /// <returns>True if odd number</returns>
        public static bool IsOdd(this double d)
        {
            string s = d.ToString();
            string n = s.Right(1);
            int i = Int32.Parse(n);
            return i % 2 != 0;
        }
        //=> d % 2 != 0;
        #endregion 

    }
}
