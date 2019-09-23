using System;
using Xunit;

namespace PrimitiveExtensions.Tests
{
    public class BooleanExtensionsTests
    {

        [Fact]
        public void ToIndicatingStrings_GivenNull_ExpectNull()
        {
             
            bool? b = null;
            string s = b.ToIndicatingStrings("yes", "no");
            Assert.Null(s);
        }
        [Fact]
        public void ToIndicatingStrings_GivenTrue_Expectyes()
        {

            bool? b = true;
            string s = b.ToIndicatingStrings("yes", "no");
            Assert.Equal("yes", s);
        }

        [Fact]
        public void ToIndicatingStrings_GivenFalse_ExpectNo()
        {

            bool? b = false;
            string s = b.ToIndicatingStrings("yes", "no");
            Assert.Equal("no", s);
        }



        [Fact]
        public void ToBoolNullable_GivenNull_ExpectNull()
        {
            string s = null; 
            bool? result = s.ToBoolNullable();
            Assert.Null(result);
        }


        [Fact]
        public void ToBoolNullable_GivenEmptyString_ExpectNull()
        {
            string s = "";
            bool? result = s.ToBoolNullable();
            Assert.Null(result);
        }

        [Fact]
        public void ToBoolNullable_GivenWhitespace_ExpectNull()
        {
            string s = "     ";
            bool? result = s.ToBoolNullable();
            Assert.Null(result);
        }

        [Fact]
        public void ToBoolNullable_true_ExpectTrue()
        {
            string s = "true";
            bool? result = s.ToBoolNullable();
            Assert.True(result);
        }

        [Fact]
        public void ToBool_GivenY_ExptectTrue()
        {
            string s = "Y";
            bool result = s.ToBool();
            Assert.True(result);
        }    

        [Fact]
        public void ToBool_GivenYeah_ExptectTrue()
        {
            string s = "Yeah";
            bool result = s.ToBool();
            Assert.True(result);
        }


        [Fact]
        public void ToBool_GivenTRUE_ExptectTrue()
        {
            string s = "TRUE";
            bool result = s.ToBool();
            Assert.True(result);
        }


        [Fact]
        public void ToBool_GivenFALSE_ExptectFalse()
        {
            string s = "FALSE";
            bool result = s.ToBool();
            Assert.False(result);
        }

        [Fact]
        public void ToBool_GivenEmptyString_ExptectException()
        {
            string s = "";
            Assert.Throws<Exception>(()=> s.ToBool());
        }

        [Fact]
        public void ToBool_GivenNullString_ExpectException()
        {
            string s = null;
            Assert.Throws<ArgumentNullException>(() => s.ToBool());
        }
        [Fact]
        public void ToBool_GivenYes_ExptectTrue()
        {
            string s = "Yes";
            bool result = s.ToBool();
            Assert.True(result);
        }

        [Fact]
        public void ToBool_Givenyes_ExptectTrue()
        {
            string s = "yes";
            bool result = s.ToBool();
            Assert.True(result);
        }
        [Fact]
        public void ToBool_Given1_ExptectTrue()
        {
            string s = "1";
            bool result = s.ToBool();
            Assert.True(result);
        }

        [Fact]
        public void ToBool_Given100string_ExptectTrue()
        {
            string s = "100";
            bool result = s.ToBool();
            Assert.True(result);
        }

        [Fact]
        public void ToBool_Given10d0string_ExptectTrue()
        {
            string s = "10.0";
            bool result = s.ToBool();
            Assert.True(result);
        }

        [Fact]
        public void ToBool_Given0string_ExptectFalse()
        {
            string s = "0";
            bool result = s.ToBool();
            Assert.False(result);
        }

        [Fact]
        public void ToBool_Given000string_ExptectFalse()
        {
            string s = "000";
            bool result = s.ToBool();
            Assert.False(result);
        }

        [Fact]
        public void ToBool_Given000d_ExptectFalse()
        {
            decimal d = 000;
            bool result = d.ToBool();
            Assert.False(result);
        }

        [Fact]
        public void ToBool_Given000integer_ExptectFalse()
        {
            int i = 000;
            bool result = i.ToBool();
            Assert.False(result);
        }

        [Fact]
        public void ToBool_Given01decimal_ExptectTrue()
        {
            decimal d = 0.1m;
            bool result = d.ToBool();
            Assert.True(result);
        }

        [Fact]
        public void ToSwitch_GivenTrue_ExpectYes()
        {
            string response = null;
            bool? b = true;
            response = b.ToSwitch("Yes", "No");
            Assert.Equal("Yes", response);
        }
        [Fact]
        public void ToSwitch_GivenFalse_ExpectNo()
        {
            string response = null;
            bool? b = false;
            response = b.ToSwitch("Yes", "No");
            Assert.Equal("No", response);
        }
        [Fact]
        public void ToSwitch_GivenNull_ExpectNull()
        {
            string response = null;
            bool? b = null;
            response = b.ToSwitch("Yes", "No");
            Assert.Null(response);
        }
    }
}
