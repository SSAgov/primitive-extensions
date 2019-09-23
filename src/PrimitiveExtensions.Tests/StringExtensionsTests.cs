using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PrimitiveExtensions.Tests
{
    public class StringExtensionsTests
    {
                [Fact]
        public void WrapHtml_GivenSammy_Tag_ExpectTagSammyTag()
        {
            string s = "Sammy";
            string result = s.Wrap("<h1>", "</h1>");
            string expectedResult = "<h1>Sammy</h1>";
            Assert.Equal(expectedResult, result);
        }



        [Fact]
        public void TruncateTextAndAddElipses_GivenStrin_ExpecString3charPlusElips()
        {
            string s = "Hello this is test for truncate after 3 character, this line will be truncated";
            string result = s.TruncateTextAndAddElipses(3);
            string expectedResult = "Hel...";
            Assert.Equal(expectedResult, result);

        }

        [Fact]
        public void TruncateTextAndAddElipses_GivenStrin_ExpecString70charPlusElips()
        {
            string s = "Hello this is test for truncate after 70 character, this line will be truncated";
            string result = s.TruncateTextAndAddElipses();
            string expectedResult = "Hello this is test for truncate after 70 character, this line will be ...";
            Assert.Equal(expectedResult, result);

        }

        [Fact]
        public void IsNullOrEmptyString_GivenString_ExpectFalse()
        {
            string s = "Hello this is test";
            bool result = s.IsNullOrEmptyString();
            bool expectedResult = false;
            Assert.Equal(expectedResult, result);
        }
        [Fact]
        public void IsNullOrEmptyString_GivenNothing_ExpectTrue()
        {
            string s = "";
            bool result = s.IsNullOrEmptyString();
            bool expectedResult = true;
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void IsEqualTo_GivenStringToCompare_True_ExpectTrue()
        {
            string s = "Hello Tester";
            bool result = s.IsEqualTo("Hello Tester", true);
            bool expectedResult = true;
            Assert.Equal(expectedResult, result);
        }
        [Fact]
        public void IsEqualTo_GivenStringToCompare_True_ExpectFalse()
        {
            string s = "HELLO Tester";
            bool result = s.IsEqualTo("Hello Tester", true);
            bool expectedResult = false;
            Assert.Equal(expectedResult, result);
        }
        [Fact]
        public void IsEqualTo_GivenStringToCompare_False_ExpectTrue()
        {
            string s = "HELLO Tester";
            bool result = s.IsEqualTo("Hello Tester", false);
            bool expectedResult = true;
            Assert.Equal(expectedResult, result);
        }
        [Fact]
        public void IsEqualTo_GivenStringToCompare_False_ExpectFalse()
        {
            string s = "HELLO Testers";
            bool result = s.IsEqualTo("Hello Tester", false);
            bool expectedResult = false;
            Assert.Equal(expectedResult, result);
        }
        // Contains
        [Fact]
        public void Contains_GivenStringToCompare_ExpectTrue()
        {
            string s = "Hello Sammy tester";
            bool result = s.Contains("Sammy");
            bool expectedResult = true;
            Assert.Equal(expectedResult, result);
        }
        [Fact]
        public void Contains_GivenStringToCompare_False_ExpectTrue()
        {
            string s = "Hello Sammy tester";
            bool result = s.Contains("SAMMY", false);
            bool expectedResult = true;
            Assert.Equal(expectedResult, result);
        }
        // StartsWith
        [Fact]
        public void StartsWith_GivenStringToCompare_False_ExpectTrue()
        {
            string s = "Hello Sammy tester";
            bool result = s.StartsWith("HELLO", false);
            bool expectedResult = true;
            Assert.Equal(expectedResult, result);
        }
        [Fact]
        public void StartsWith_GivenStringToCompare_True_ExpectFalse()
        {
            string s = "Hello Sammy tester";
            bool result = s.StartsWith("HELLO", true);
            bool expectedResult = false;
            Assert.Equal(expectedResult, result);
        }
        //EndsWith
        [Fact]
        public void EndsWith_GivenStringToCompare_True_ExpectTrue()
        {
            string s = "Hello Sammy";
            bool result = s.EndsWith("Sammy", true);
            bool expectedResult = true;
            Assert.Equal(expectedResult, result);
        }
        [Fact]
        public void EndsWith_GivenStringToCompare_False_ExpectTrue()
        {
            string s = "Hello Sammy";
            bool result = s.EndsWith("SAMMY", false);
            bool expectedResult = true;
            Assert.Equal(expectedResult, result);
        }
        //IsGreaterThan
        [Fact]
        public void IsGreaterThan_GivenStringToCompare_ExpectFalse()
        {
            string s = "Sammy";
            bool result = s.IsGreaterThan("SAMMY");
            bool expectedResult = false;
            Assert.Equal(expectedResult, result);
        }
        [Fact]
        public void IsGreaterThan_GivenStringToCompare_ExpectTrue()
        {
            string s = "Hello Sammy ";
            bool result = s.IsGreaterThan("Hello");
            bool expectedResult = true;
            Assert.Equal(expectedResult, result);
        }
        //IsGreaterThanOrEqualTo
        [Fact]
        public void IsGreaterThanOrEqualTo_GivenStringToCompare_ExpectTrue()
        {
            string s = "Hello Sammy ";
            bool result = s.IsGreaterThanOrEqualTo("Hello");
            bool expectedResult = true;
            Assert.Equal(expectedResult, result);
        }
        [Fact]
        public void IsGreaterThanOrEqualTo_GivenStringToCompare_ExpectTrue2()
        {
            string s = "Sammy ";
            bool result = s.IsGreaterThanOrEqualTo("Hello Sammy");
            bool expectedResult = true;
            Assert.Equal(expectedResult, result);
        }
        //IsLessThan
        [Fact]
        public void IsLessThan_GivenStringToCompare_ExpectFalse()
        {
            string s = "Sammy";
            bool result = s.IsLessThan("Hello Sammy");
            bool expectedResult = false;
            Assert.Equal(expectedResult, result);
        }
        [Fact]
        public void IsLessThan_GivenStringToCompare_ExpectTrue()
        {
            string s = "Hello Sammy";
            bool result = s.IsLessThan("Sammy");
            bool expectedResult = true;
            Assert.Equal(expectedResult, result);
        }
        //IsLessThanOrEqualTo
        [Fact]
        public void IsLessThanOrEqualTo_GivenStringToCompare_ExpectFalse()
        {
            string s = "Sammy";
            bool result = s.IsLessThanOrEqualTo("Hello Sammy");
            bool expectedResult = false;
            Assert.Equal(expectedResult, result);
        }
        [Fact]
        public void IsLessThanOrEqualTo_GivenStringToCompare_ExpectTrue()
        {
            string s = "Hello Sammy";
            bool result = s.IsLessThanOrEqualTo("Sammy");
            bool expectedResult = true;
            Assert.Equal(expectedResult, result);
        }
        //IsEqualToAnyOfTheFollowing
        [Fact]
        public void IsEqualToAnyOfTheFollowing_GivenListToCompare_ExpectFalse()
        {
            List<string> s = new List<string>();
            s.Add("one");
            s.Add("two");
            s.Add("three");
            bool result = "Hello".IsEqualToAnyOfTheFollowing(s);
            bool expectedResult = false;
            Assert.Equal(expectedResult, result);

        }
        [Fact]
        public void IsEqualToAnyOfTheFollowing_GivenListToCompare_ExpectTrue()
        {
            List<string> s = new List<string>();
            s.Add("one");
            s.Add("two");
            s.Add("three");
            bool result = "one".IsEqualToAnyOfTheFollowing(s);
            bool expectedResult = true;
            Assert.Equal(expectedResult, result);

        }
        [Fact]
        public void IsEqualToAnyOfTheFollowing_GivenListToCompare_ExpectTrue2()
        {
            List<string> s = new List<string>();
            s.Add("one");
            s.Add("two");
            s.Add("three");
            bool result = "two".IsEqualToAnyOfTheFollowing(s);
            bool expectedResult = true;
            Assert.Equal(expectedResult, result);

        }
        // ContainsAnyOfTheFollowing
        [Fact]
        public void ContainsAnyOfTheFollowing_GivenListToCompare_ExpectFalse()
        {
            List<string> s = new List<string>();
            s.Add("one");
            s.Add("two");
            s.Add("three");
            bool result = "Hello".ContainsAnyOfTheFollowing(s);
            bool expectedResult = false;
            Assert.Equal(expectedResult, result);

        }
        [Fact]
        public void ContainsAnyOfTheFollowing_GivenListToCompare_ExpectTrue()
        {
            List<string> s = new List<string>();
            s.Add("one");
            s.Add("two");
            s.Add("three");
            bool result = "one".ContainsAnyOfTheFollowing(s);
            bool expectedResult = true;
            Assert.Equal(expectedResult, result);

        }
        [Fact]
        public void ContainsAnyOfTheFollowing_GivenListToCompare_ExpectTrue2()
        {
            List<string> s = new List<string>();
            s.Add("one");
            s.Add("two");
            s.Add("three");
            bool result = "two".ContainsAnyOfTheFollowing(s);
            bool expectedResult = true;
            Assert.Equal(expectedResult, result);

        }
        //StartsWithAnyOfTheFollowing
        [Fact]
        public void StartsWithAnyOfTheFollowing_GivenListToCompare_ExpectTrue()
        {
            List<string> s = new List<string>();
            s.Add("one");
            s.Add("two");
            s.Add("three");
            bool result = "one".StartsWithAnyOfTheFollowing(s);
            bool expectedResult = true;
            Assert.Equal(expectedResult, result);

        }
        //IsANumber
        [Fact]
        public void IsANumber_GivenNumber_ExpectTrue()
        {
            string s = "007";
            bool result = s.IsANumber();
            bool expectedResult = true;
            Assert.Equal(expectedResult, result);
        }
        [Fact]
        public void IsANumber_GivenString_ExpectFalse()
        {
            string s = "Bond";
            bool result = s.IsANumber();
            bool expectedResult = false;
            Assert.Equal(expectedResult, result);
        }
        // IsAnInteger
        [Fact]
        public void IsAnInteger_GivenNumber_ExpectTrue()
        {
            string s = "007";
            bool result = s.IsAnInteger();
            bool expectedResult = true;
            Assert.Equal(expectedResult, result);
        }
        [Fact]
        public void IsAnInteger_GivenDecimal_ExpectFalse()
        {
            string s = "3.14";
            bool result = s.IsAnInteger();
            bool expectedResult = false;
            Assert.Equal(expectedResult, result);
        }
        [Fact]
        public void IsAnInteger_GivenNothing_ExpectFalse()
        {
            string s = "";
            bool result = s.IsAnInteger();
            bool expectedResult = false;
            Assert.Equal(expectedResult, result);
        }
        [Fact]
        public void IsAnInteger_GivenNull_ExpectFalse()
        {
            string s = null;
            bool result = s.IsAnInteger();
            bool expectedResult = false;
            Assert.Equal(expectedResult, result);
        }

        //IsALongInteger
        [Fact]
        public void IsALongInteger_GivenLongInt_ExpectTrue()
        {
            string s = "4294967290";  //max 4294967295
            bool result = s.IsALongInteger();
            bool expectedResult = true;
            Assert.Equal(expectedResult, result);
        }
        [Fact]
        public void IsALongInteger_GivenInt_ExpectFalse()
        {
            string s = "42949672955";
            bool result = s.IsALongInteger();
            bool expectedResult = false;
            Assert.Equal(expectedResult, result);
        }
        [Fact]
        public void IsALongInteger_GivenDecimal_ExpectFalse()
        {
            string s = "429496729.2";
            bool result = s.IsALongInteger();
            bool expectedResult = false;
            Assert.Equal(expectedResult, result);
        }
        //IsADecimal
        [Fact]
        public void IsADecimal_GivenDecimal_ExpectTrue()
        {
            string s = "3.14";
            bool result = s.IsADecimal();
            bool expectedResult = true;
            Assert.Equal(expectedResult, result);
        }
        [Fact]
        public void IsADecimal_GivenInt_ExpectTrue()
        {
            // IsADecimal return true if converted, does not check if the number is int or not
            string s = "30140";
            bool result = s.IsADecimal();
            bool expectedResult = true;
            Assert.Equal(expectedResult, result);

        }

        [Fact]
        public void IsADecimal_GivenNothing_ExpectFalse()
        {
            // IsADecimal return true if converted, does not check if the number is int or not
            string s = "";
            bool result = s.IsADecimal();
            bool expectedResult = false;
            Assert.Equal(expectedResult, result);

        }
        [Fact]
        public void IsADecimal_GivenNull_ExpectFalse()
        {
            // IsADecimal return true if converted, does not check if the number is int or not
            string s = null;
            bool result = s.IsADecimal();
            bool expectedResult = false;
            Assert.Equal(expectedResult, result);

        }

        //IsAnEmailAddress
        [Fact]
        public void IsAnEmailAddress_GivenValidEmail_ExpectTrue()
        {
            string s = "first.last@myemail.com";
            bool result = s.IsAnEmailAddress();
            bool expectedResult = true;
            Assert.Equal(expectedResult, result);
        }
        [Fact]
        public void IsAnEmailAddress_GivenInValidEmail_ExpectFalse()
        {
            string s = "first.last.myemail.com";
            bool result = s.IsAnEmailAddress();
            bool expectedResult = false;
            Assert.Equal(expectedResult, result);
        }
        // Reverse
        [Fact]
        public void Reverse_GivenString_ExpectStringResversed()
        {
            string s = "1234567";
            string result = s.Reverse();
            string expectedResult = "7654321";
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void ReplaceFirstOccurence_GivenSting_x_S_ExpectStringWith_S()
        {
            string s = "xammy Abaxa";
            string result = s.ReplaceFirstOccurence("x", "S");
            string expectedResult = "Sammy Abaxa";
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void ReplaceFirstOccurence_GivenEmptyString_x_S_ExpectStringWith_S()
        {
            string s = "";
            string result = s.ReplaceFirstOccurence("x", "S");
            string expectedResult = "";
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void ReplaceFirstOccurence_Given_aaa_x_S_ExpectStringWith_Saa()
        {
            string s = "aaa";
            string result = s.ReplaceFirstOccurence("a", "S");
            string expectedResult = "Saa";
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void ReplaceFirstOccurence_Given_aaa_a_APPLE_ExpectStringWith_APPLEaa()
        {
            string s = "aaa";
            string result = s.ReplaceFirstOccurence("a", "APPLE");
            string expectedResult = "APPLEaa";
            Assert.Equal(expectedResult, result);
        }

        //Repeat
        [Fact]
        public void Repeat_Given3_ExpectString_repeated3Times()
        {
            string s = "A1 ";
            string result = s.Repeat(3);
            string expectedResult = "A1 A1 A1 ";
            Assert.Equal(expectedResult, result);
        }
        //ReplaceCharacters
        [Fact]
        public void ReplaceCharacters_GivenString_ExpectStringReplacedWithGivenParam()
        {
            string s = "Hello?Open?Source?Team";
            string result = s.ReplaceCharacters("?");
            string expectedResult = "Hello_Open_Source_Team";
            Assert.Equal(expectedResult, result);
        }
        [Fact]
        public void ReplaceCharacters_GivenString_ExpectStringReplacedWithGivenParam2()
        {
            string s = "Hello?Open?Source?Team";
            char[] charsToReplace = { '?', 'u' };
            string result = s.ReplaceCharacters(charsToReplace);
            string expectedResult = "Hello_Open_So_rce_Team";
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void ReplaceCharacters_GivenString_ExpectStringReplacedWithGivenParam3()
        {
            string s = "Hello?Open?Source?Team";
            string charsToReplace = "?u";
            string result = s.ReplaceCharacters(charsToReplace);
            string expectedResult = "Hello_Open_So_rce_Team";
            Assert.Equal(expectedResult, result);
        }


        //ChopOffEnd
        [Fact]

        public void ChopOffEnd_GivenHello_space_Greeting_ExpectHello()
        {
            string s = "Hello Greeting";
            string result = s.ChopOffEnd();
            string expectedResult = "Hello";
            Assert.Equal(expectedResult, result);
        }
        [Fact]
        public void ChopOffEnd_GivenHello_hyphen_Greeting_ExpectHello()
        {
            string s = "Hello-Greeting";
            string result = s.ChopOffEnd();
            string expectedResult = "Hello";
            Assert.Equal(expectedResult, result);
        }
        
        [Fact]
            public void SplitCamelCase_Given_HelloThereHowAreYou_Expect_5()
        {
            string s = "HelloThereHowAreYou";

            string[] strings = s.SplitCamelCase();

            Assert.Equal(5, strings.Count());
        }
        [Fact]
        public void SplitCamelCase_Given_helloThereHowAreYou_Expect_5()
        {
            string s = "helloThereHowAreYou";

            string[] strings = s.SplitCamelCase();

            Assert.Equal(5, strings.Count());
        }

        [Fact]
        public void SplitCamelCase_Given_hello_ThereHowAreYou_Expect_5()
        {
            string s = "hello ThereHowAreYou";

            string[] strings = s.SplitCamelCase();

            Assert.Equal(5, strings.Count());
            Assert.Equal("hello ", strings[0]);
        }

        //ToTitleCase
        [Fact]
        public void ToTitleCase_GivenString_ExpectStringWithFirstletterCap()
        {
            string s = "aL bundY";
            string result = s.ToTitleCase();
            string expectedResult = "Al Bundy";
            Assert.Equal(expectedResult, result);
        }
        //Right
        [Fact]
        public void Right_GivenString_ExpectStringRightWord()
        {
            string s = "Al Bundy";
            string result = s.Right(5);
            string expectedResult = "Bundy";
            Assert.Equal(expectedResult, result);
        }
        //Left
        [Fact]
        public void Left_GivenString_param_ExpectStringLefttWord()
        {
            string s = "Al Bundy";
            string result = s.Left(4);
            string expectedResult = "Al B";
            Assert.Equal(expectedResult, result);
        }
        //LastWord
        [Fact]
        public void LastWord_GivenString_True_ExpectStringLastWord()
        {
            string s = "Al Bundy 7";
            string result = s.LastWord(true);
            string expectedResult = "Bundy";
            Assert.Equal(expectedResult, result);
        }
        [Fact]
        public void LastWord_GivenString_True_ExpectStringLastWordIncludeNumber()
        {
            string s = "Al Bundy 7";
            string result = s.LastWord();
            string expectedResult = "7";
            Assert.Equal(expectedResult, result);
        }
        //ExtractNumberFromString
        [Fact]
        public void ExtractNumberFromString_GivenStringWithNumber_ExpectNumber()
        {
            string s = "This is my phone (7777)";
            char l = '(';
            char r = ')';
            int result = s.ExtractNumberFromString(l,r);
            int expectedResult = 7777;
            Assert.Equal(expectedResult, result);
        }
        //ExtractNumberFromBeginning
        [Fact]
        public void ExtractNumberFromBeginning_GivenStringWithNumber_ExpectNumber()
        {
            string s = "A 7777 This is my phone ";
            int result = s.ExtractNumberFromBeginning();
            int expectedResult = 7777;
            Assert.Equal(expectedResult, result);
        }
        //RemoveDatatypeLength
        [Fact]
        public void RemoveDatatypeLength_GivenStringWithDataTypeLength_ExpectStingNoDataTypeLength()
        {
            string s = "Varchar(100)";
            string result = s.RemoveDatatypeLength('(');
            s.ExtractNumberFromBeginning();
            string expectedResult = "Varchar";
            Assert.Equal(expectedResult, result);
        }
        //SumOfCharNumbers
        [Fact]
        public void SumOfCharNumbers_GivenNumbers_ExpectSumOfnumbers()
        {
            string s = "123";
            long result = s.SumOfCharNumbers();
            long expectedResult = 150;
            Assert.Equal(expectedResult, result);
        }
        //RemoveAfter
        [Fact]
        public void RemoveAfter_GivenString_ExpectStingAfterTheFirstword()
        {
            string s = "Hello Al Bundy";
            string result = s.RemoveAfter(6);
            string expectedResult = "Hello";
            Assert.Equal(expectedResult, result);
        }
        //RemoveAfterFirst
        [Fact]
        public void RemoveAfterFirst_GivenAl_ExpectStingAfterTheFirstAl()
        {
            string s = "Hello Al Bundy";
            string result = s.RemoveAfterFirst("Al");
            string expectedResult = "Hello Al";
            Assert.Equal(expectedResult, result);
        }

        //RemoveFirstAndAfter
        [Fact]
        public void RemoveFirstAndAfter_GivenAl_ExpectStingAfterTheFirstAl()
        {
            string s = "Hello Al Bundy";
            string result = s.RemoveFirstAndAfter("Al");
            string expectedResult = "Hello ";
            Assert.Equal(expectedResult, result);
        }

        //RemoveLastAndAfter
        [Fact]
        public void RemoveLastAndAfter_GivenAl_ExpectStingBeforeLastAl()
        {
            string s = "Hello Al, and Al Bundy";
            string result = s.RemoveLastAndAfter("Al");
            string expectedResult = "Hello Al, and ";
            Assert.Equal(expectedResult, result);
        }
        //RemoveBeforeFirst
        [Fact]
        public void RemoveBeforeFirst_GivenAl_ExpectStingBeforeFirstAl()
        {
            string s = "Hello Al, and Al Bundy";
            string result = s.RemoveBeforeFirst("Al");
            string expectedResult = "Al, and Al Bundy";
            Assert.Equal(expectedResult, result);
        }
        //RemoveBeforeLast
        [Fact]
        public void RemoveBeforeLast_GivenAl_ExpectStingAfterLastAl()
        {
            string s = "Hello Al, and Al Bundy";
            string result = s.RemoveBeforeLast("Al");
            string expectedResult = " Bundy";
            Assert.Equal(expectedResult, result);
        }
        //RemoveBefore
        [Fact]
        public void RemoveBefore_Given6_ExpectStingAfterFirstAl()
        {
            string s = "Hello Al, and Al Bundy";
            string result = s.RemoveBefore(6);
            string expectedResult = "Al, and Al Bundy";
            Assert.Equal(expectedResult, result);
        }
        //IsNotNullOrEmptyString
        [Fact]
        public void IsNotNullOrEmptyString_GivenString_ExpectTrue()
        {
            string s = "Hello Al, and Al Bundy";
            bool result = s.IsNotNullOrEmptyString();
            bool expectedResult = true;
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void IsNumeric_GivenHello_ExpectFalse()
        {
            string s = "Hello";
            bool result = s.IsANumber();
            bool expectedResult = false;
            Assert.Equal(expectedResult, result);
        }
        [Fact]
        public void IsNumeric_Given123_ExpectTrue()
        {
            string s = "123";
            bool result = s.IsANumber();
            bool expectedResult = true;
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void IsNumeric_GivenNothing_ExpectFalse()
        {
            string s = "";
            bool result = s.IsANumber();
            bool expectedResult = false;
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void IsNumeric_GivenDecimal_ExpectTrue()
        {
            string s = "-12.3";
            bool result = s.IsANumber();
            bool expectedResult = true;
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void IsNumeric_GivenNull_ExpectTrue()
        {
            string s = null;
            bool result = s.IsANumber();
            bool expectedResult = false;
            Assert.Equal(expectedResult, result);
        }



    }
}
