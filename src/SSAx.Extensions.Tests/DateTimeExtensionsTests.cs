using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SSAx.PrimitiveExtensions.Tests
{
    public class DateTimeExtensionsTests
    {
        [Fact]
        public void EncodeAsSqlVariable_GivenDate_PrecisionToYear_ExpectMatch()
        {
            DateTime d = new DateTime(2003, 4, 17, 8, 37, 12, 123);
            string result = d.EncodeAsSqlVariable(DateTimeExtensions.DateTimePrecision.Year);
            Assert.Equal("'2003-01-01'", result);
        }

        [Fact]
        public void EncodeAsSqlVariable_GivenDate_PrecisionToMonth_ExpectMatch()
        {
            DateTime d = new DateTime(2003, 4, 17, 8, 37, 12, 123);
            string result = d.EncodeAsSqlVariable(DateTimeExtensions.DateTimePrecision.Month);
            Assert.Equal("'2003-04-01'", result);
        }


        [Fact]
        public void EncodeAsSqlVariable_GivenDate_PrecisionToDay_ExpectMatch()
        {
            DateTime d = new DateTime(2003, 4, 17, 8, 37, 12, 123);
            string result = d.EncodeAsSqlVariable(DateTimeExtensions.DateTimePrecision.Day);
            Assert.Equal("'2003-04-17'", result);
        }

        [Fact]
        public void EncodeAsSqlVariable_GivenDate_PrecisionToHour_ExpectMatch()
        {
            DateTime d = new DateTime(2003, 4, 17, 8, 37, 12, 123);
            string result = d.EncodeAsSqlVariable(DateTimeExtensions.DateTimePrecision.Hour);
            Assert.Equal("'2003-04-17 08:00:00'", result);
        }

        [Fact]
        public void EncodeAsSqlVariable_GivenDate_PrecisionToMinute_ExpectMatch()
        {
            DateTime d = new DateTime(2003, 4, 17, 8, 37, 12, 123);
            string result = d.EncodeAsSqlVariable(DateTimeExtensions.DateTimePrecision.Minute);
            Assert.Equal("'2003-04-17 08:37:00'", result);
        }



        [Fact]
        public void EncodeAsSqlVariable_GivenDate_PrecisionToSecond_ExpectMatch()
        {
            DateTime d = new DateTime(2003, 4, 17, 8, 37, 12, 123);
            string result = d.EncodeAsSqlVariable(DateTimeExtensions.DateTimePrecision.Second);
            Assert.Equal("'2003-04-17 08:37:12'", result);
        }

        [Fact]
        public void EncodeAsSqlVariable_GivenDate_PrecisionToMilliSecond_ExpectMatch()
        {
            DateTime d = new DateTime(2003, 4, 17, 8, 37, 12, 123);
            string result = d.EncodeAsSqlVariable(DateTimeExtensions.DateTimePrecision.Millisecond);
            Assert.Equal("'2003-04-17 08:37:12.123'", result);
        }
    }
}