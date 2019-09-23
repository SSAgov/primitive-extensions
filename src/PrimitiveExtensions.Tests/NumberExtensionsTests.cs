using Xunit;

namespace PrimitiveExtensions.Tests
{
    public class NumberExtensionsTests

    {
        [Fact]
        public void IsOdd_Given13_byte_ExpectTrue()
        {
            byte b = 13;
            Assert.True(b.IsOdd());
        }

        [Fact]
        public void IsOdd__Given17_int_ExpectTrue()
        {
            int i = 7;
            bool result = i.IsOdd();
            bool expectedResult = true;
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void IsOdd__Given14_int_ExpectFalse()
        {
            int i = 14;
            bool result = i.IsOdd();
            bool expectedResult = false;
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void IsOdd_Given13_short_ExpectTrue()
        {
            short s = 13;
            Assert.True(s.IsOdd());
        }

        [Fact]
        public void IsOdd_Given13_ushort_ExpectTrue()
        {
            ushort s = 13;
            Assert.True(s.IsOdd());
        }

        [Fact]
        public void IsOdd_Given13_int_ExpectTrue()
        {
            int i = -13;
            Assert.True(i.IsOdd());
        }

        [Fact]
        public void IsOdd_Given13_uint_ExpectTrue()
        {
            uint i = 13;
            Assert.True(i.IsOdd());
        }

        [Fact]
        public void IsOdd_Given13_long_ExpectTrue()
        {
            long l = 13;
            Assert.True(l.IsOdd());
        }
        [Fact]
        public void IsOdd_Given13_ulong_ExpectTrue()
        {
            ulong l = 13;
            Assert.True(l.IsOdd());
        }

        [Fact]
        public void IsOdd_Given7dot7_ulong_ExpectTrue()
        {
            double d = 7.7;
            bool result = d.IsOdd();
            bool expectedResult = true;
            Assert.Equal(expectedResult, result);
        }


        [Fact]
        public void IsEven_Given12_byte_ExpectTrue()
        {
            byte b = 12;
            Assert.True(b.IsEven());
        }



        [Fact]
        public void IsEven_Given12_sbyte_ExpectTrue()
        {
            sbyte b = 12;
            Assert.True(b.IsEven());
        }

        [Fact]
        public void IsEven_Given12_short_ExpectTrue()
        {
            short s = 12;
            Assert.True(s.IsEven());
        }

        [Fact]
        public void IsEven_Given12_ushort_ExpectTrue()
        {
            ushort s = 12;
            Assert.True(s.IsEven());
        }

        [Fact]
        public void IsEven_Given12_int_ExpectTrue()
        {
            int i = -12;
            Assert.True(i.IsEven());
        }

        [Fact]
        public void IsEven_Given12_uint_ExpectTrue()
        {
            uint i = 12;
            Assert.True(i.IsEven());
        }

        [Fact]
        public void IsEven_Given12_long_ExpectTrue()
        {
            long l = 12;
            Assert.True(l.IsEven());
        }
        [Fact]
        public void IsEven_Given12_ulong_ExpectTrue()
        {
            ulong l = 12;
            Assert.True(l.IsEven());
        }
        [Fact]
        public void IsOdd_Given14dot4_double_ExpectFalse()
        {
            double d = 14.4;
            bool result = d.IsOdd();
            bool expectedResult = false;
            Assert.Equal(expectedResult, result);
        }
        [Fact]
        public void IsOdd_Given5dot5_double_ExpectTrue()
        {
            double d = 5.5;
            bool result = d.IsOdd();
            bool expectedResult = true;
            Assert.Equal(expectedResult, result);
        }


        [Fact]
        public void IsEven_Given14dot4_double_ExpectTrue()
        {
            double d = 14.4;
            bool result = d.IsEven();
            bool expectedResult = true;
            Assert.Equal(expectedResult, result);
        }
        //
        [Fact]
        public void IsEven_Given14dot45_double_ExpectFalse()
        {
            double d = 14.45;
            bool result = d.IsEven();
            bool expectedResult = false;
            Assert.Equal(expectedResult, result);
        }
    }
}