using System;
using Xunit;

namespace PrimitiveExtensions.Tests
{
    public class ObjectExtensionsTests
    {
        [Fact]
        public void EncodeAsSqlVariable_string_with_tic()
        {
            string s = "O'clock";
            string result = s.EncodeAsSqlVariable();
            string expectedResult = "'O''clock'";
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void EncodeAsSqlVariable_string()
        {
            string s = "hello";
            string result = s.EncodeAsSqlVariable();
            string expectedResult = "'hello'";
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void EncodeStringAsSqlVariable_date()
        {
            DateTime dt = new DateTime(2020, 12, 31);
            string result = dt.EncodeAsSqlVariable();
            string expectedResult = "'2020-12-31'";
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void EncodeStringAsSqlVariable_datetime()
        {
            DateTime dt = new DateTime(2020, 12, 31,16,36,30);
            string result = dt.EncodeAsSqlVariable();
            string expectedResult = "'2020-12-31 16:36:30'";
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void EncodeStringAsSqlVariable_double()
        {
            double d = 1.2345;
            string result = d.EncodeAsSqlVariable();
            string expectedResult = "1.2345";
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void EncodeStringAsSqlVariable_int()
        {
            int i = 1000;
            string result = i.EncodeAsSqlVariable();
            string expectedResult = "1000";
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void EncodeStringAsSqlVariable_null()
        {
            object o = null;
            string result = o.EncodeAsSqlVariable();
            string expectedResult = "NULL";
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void EncodeStringAsSqlVariable_dbnull()
        {
            object o = DBNull.Value;
            string result = o.EncodeAsSqlVariable();
            string expectedResult = "NULL";
            Assert.Equal(expectedResult, result);
        }

        #region NzString Tests
        [Fact]
        public void NzString_GivenNullObject_ExpectEmptyString()
        {
            object o = null;
            string result = o.NzString();
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void NzString_GivenValidString_ExpectThatString()
        {
            string s = "Hello";
            string result = s.NzString();
            Assert.Equal("Hello", result);
        }

        [Fact]
        public void NzString_GivenNullString_ExpectThatString()
        {
            string s = null;
            string result = s.NzString();
            Assert.Equal("", result);
        }


        #endregion

        #region NzInt Tests


        [Fact]
        public void NzInt_GivenString123_Expect123()
        {
            int i = "123".NzInt();
            Assert.Equal(123, i);
        }
        [Fact]
        public void NzInt_GivenString_test()
        {
            int i = "".NzInt();
            Assert.Equal(0, i);
        }




        [Fact]
        public void NzInt_GivenNewDateTime_Expect0()
        {
            DateTime dt = new DateTime();
            int i = dt.NzInt();
            Assert.Equal(0, i);
        }

        [Fact]
        public void NzInt_GivenNullObject_Expect0()
        {
            object o = null;
            int i = o.NzInt();
            i = Convert.ToInt32(null);
            Assert.Equal(0, i);
        }

        [Fact]
        public void NzInt_GivenString123456789123456789_Expect0()
        {
            string s = "123456789123456789";
            int i = s.NzInt();
            
            Assert.Equal(0, i);
        }

        [Fact]
        public void NzInt_GivenString123456_Expect123456()
        {
            string s = "123456";
            int i = s.NzInt();
            Assert.Equal(123456, i);
        }

        [Fact]
        public void NzInt_GivenDouble123456_Expect123456()
        {
            double d = 123456;
            int i = d.NzInt();
            Assert.Equal(123456, i);
        }

        [Fact]
        public void NzInt_GivenDecimal123456_Expect123456()
        {
            decimal d = 123456;
            int i = d.NzInt();
            Assert.Equal(123456, i);
        }

        [Fact]
        public void NzInt_GivenDecimal123456_7_Expect123457()
        {
            decimal d = 123456.7m;
            int i = d.NzInt();
            Assert.Equal(123457, i);
        }

        [Fact]
        public void NzInt_GivenDbNull_Expect0()
        {
            object o = DBNull.Value;
            int i = o.NzInt();
            Assert.Equal(0, i);
        }
        #endregion

        #region NzLong Tests


        [Fact]
        public void NzLong_GivenString123_Expect123()
        {
            long l = "123".NzLong();
            Assert.Equal(123, l);
        }

        [Fact]
        public void NzLong_GivenNewDateTime_Expect0()
        {
            DateTime dt = new DateTime();
            long l = dt.NzLong();
            Assert.Equal(0, l);
        }

        [Fact]
        public void NzLong_GivenNullObject_Expect0()
        {
            object o = null;
            long l = o.NzLong();
            Assert.Equal(0, l);
        }

        [Fact]
        public void NzLong_GivenString123456789123456789_Expect0()
        {
            string s = "123456789123456789";
            long l = s.NzLong();
            Assert.Equal(123456789123456789, l);
        }

        [Fact]
        public void NzLong_GivenDbNull_Expect0()
        {
            object o = DBNull.Value;
            long l = o.NzLong();
            Assert.Equal(0, l);
        }
        #endregion

        #region NzDate Tests
        [Fact]
        public void NzDate_GivenDbNull_ExpectNewDateTime()
        {
            object o = DBNull.Value;
            DateTime dt = o.NzDateTime();
            Assert.Equal(new DateTime(), dt);
        }

        [Fact]
        public void NzDate_GivenNull_ExpectNewDateTime()
        {
            object o = null;
            DateTime result = o.NzDateTime();
            DateTime expected = new DateTime();
            Assert.Equal(expected, result);
        }

        [Fact]
        public void NzDate_GivenStringDate_ExpectDateTime()
        {
            string s = "2014-4-4";
            DateTime result = s.NzDateTime();
            DateTime expected = new DateTime(2014, 4, 4);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void NzDate_GivenEmptyString_ExpectDateTime()
        {
            string s = "";
            DateTime result = s.NzDateTime();
            DateTime expected = new DateTime();
            Assert.Equal(expected, result);
        }
        [Fact]
        public void NzDate_GivenNull_ExpectDateTime()
        {
            string s = null;
            DateTime result = s.NzDateTime();
            DateTime expected = new DateTime();
            Assert.Equal(expected, result);
        }


        [Fact]
        public void NzDate_GivenInvalidStringDate_ExpectNewDateTime()
        {
            string s = "2014-304-04";
            DateTime result = s.NzDateTime();
            DateTime expected = new DateTime();
            Assert.Equal(expected, result);
        }

        [Fact]
        public void NzDate_GivenIntLargerThanMaxYear_ExpectNewDateTime()
        {
            int i = 123456;
            DateTime result = i.NzDateTime();
            DateTime expected = new DateTime();
            Assert.Equal(expected, result);
        }

        [Fact]
        public void NzDate_GivenNegativeInt_ExpectNewDateTime()
        {
            int i = -123;
            
            DateTime result = i.NzDateTime();
            DateTime expected = new DateTime();
            Assert.Equal(expected, result);
        }

        [Fact]
        public void NzDate_GivenYearMonth_ExpectNewDateTimeWithDefaultedDay1()
        {
            string s = "2014-03";
            DateTime result = s.NzDateTime();
            DateTime expected = new DateTime(2014, 3, 1);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void NzDate_GivenYear_ExpectNewDateTimeWithDefaultedMonthAndDay1()
        {
            string s = "2014";

            DateTime result = s.NzDateTime();
            DateTime expected = new DateTime(2014, 1, 1);
            Assert.Equal(expected, result);
        }
        #endregion

        [Fact]
        public void ToNullableDateTime_GivenStringDate_ExpectValidDateTime()
        {
            DateTime? result = "2010-1-1".ToNullableDateTime();
            DateTime expected = new DateTime(2010, 1, 1);
            Assert.Equal(expected, result.Value);
        }

        [Fact]
        public void ToNullableDateTime_GivenEmpty_ExpectValidDateTime()
        {
            DateTime? result = "".ToNullableDateTime();
            DateTime expected = new DateTime();
            Assert.Equal(expected, result.Value);
        }


        [Fact]
        public void ToNullableDateTime_GivenNull_ExpectNull()
        {
            string x = null;
            DateTime? result = x.ToNullableDateTime();
            Assert.Null(result);
            
        }

    }
} 

