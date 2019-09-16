using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;


namespace SSAx.PrimitiveExtensions
{
    /// <summary>
    /// This is a static class which extends the string object/type
    /// 
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// This method will return a string wrapped in user input tag
        /// </summary>
        /// <param name="s">Is the given string</param>
        /// <returns>Return string in custom string</returns>
        public static string Wrap(this string s, string leftWrapper, string rightWrapper)
        {
            return leftWrapper + s + rightWrapper;
        }

        /// <summary>
        /// This method will return a string wrapped in user input tag
        /// </summary>
        /// <param name="s">Is the given string</param>
        /// <returns>Return string in custom string</returns>
        public static string Wrap(this string s, string wrapper)
        {
            return Wrap(wrapper, wrapper);
        }

        ///// <summary>
        /////  ??? 1
        ///// </summary>
        ///// <param name="s"></param>
        ///// <param name="t"></param>
        ///// <param name="trimSpaces"></param>
        ///// <param name="dateTimeIsDateWhenMidnight"></param>
        ///// <returns></returns>
        //public static string EncodeAsSqlVariable(this string s, Type t, bool trimSpaces = true, bool dateTimeIsDateWhenMidnight = true)
        //{
        //    string encodedString = s;
        //    string columnTypeAsString = t.ToString();

        //    if (encodedString != "NULL")
        //    {
        //        switch (columnTypeAsString.Trim().ToLower())
        //        {
        //            case "system.string":
        //                if (trimSpaces) s = s.TrimEnd();
        //                s = s.Replace("'", "''");
        //                encodedString = string.Format("'{0}'", s);

        //                break;
        //            case "system.datetime":

        //                DateTime dt = DateTime.Parse(s);
        //                if (dt.TimeOfDay == new TimeSpan() & dateTimeIsDateWhenMidnight)
        //                {
        //                    encodedString = string.Format("'{0}'", dt.ToString("yyyy-MM-dd"));
        //                }
        //                else
        //                {
        //                    encodedString = string.Format("'{0}'", dt.ToString("yyyy-MM-dd HH:mm:ss"));
        //                }


        //                break;

        //            default:

        //                break;
        //        }
        //    }
        //    return encodedString;
        //}
        

        /// <summary>
        /// This method will replace characters with ... for string greater than 70 characters
        /// </summary>
        /// <param name="s"></param>
        /// <param name="maxLengthToKeep"></param>
        /// <returns>String with "..." if the string is > 70 characters </returns>
        public static string TruncateTextAndAddElipses(this string s, int maxLengthToKeep = 70)
        {
            return s.TruncateAndAddEndingText(maxLengthToKeep, "...");
        }

        /// <summary>
        ///  This method will replace a string with input character after specified length
        /// </summary>
        /// <param name="s"></param>
        /// <param name="maxLengthToKeep"></param>
        /// <param name="endingText"></param>
        /// <returns>String ending with user defined char after specified length</returns>
        public static string TruncateAndAddEndingText(this string s, int maxLengthToKeep, string endingText)
        {
            if (s == null)
                return null;

            if (s.Length > maxLengthToKeep)
                s = s.Substring(0, maxLengthToKeep) + endingText;

            return s;
        }

        /// <summary>
        /// Returns true if the string is null or empty
        /// </summary>
        /// <param name="s"></param>
        /// <returns> True if the string is null or empty</returns>
        public static bool IsNullOrEmptyString(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        /// <summary>
        /// Returns true if the string is Not null or empty
        /// </summary>
        /// <param name="s"></param>
        /// <returns>True if the string is Not null or empty</returns>
        public static bool IsNotNullOrEmptyString(this string s)
        {
            return !IsNullOrEmptyString(s);
        }

        /// <summary>
        /// This method retrun true if two strings are equal
        /// </summary>
        /// <param name="s"></param>
        /// <param name="otherString"></param>
        /// <param name="caseSensitive"></param>
        /// <returns>True if the two strings are equal</returns>
        public static bool IsEqualTo(this string s, string otherString, bool caseSensitive = false)
        {
            if (caseSensitive)
            {
                return s.Equals(otherString);
            }
            else
            {
                return s.ToLower().Equals(otherString.ToLower());
            }
        }

        /// <summary>
        /// Returns true if the first string is contained in the second string
        /// </summary>
        /// <param name="s"></param>
        /// <param name="otherString"></param>
        /// <param name="caseSensitive"></param>
        /// <returns>True if the first string is contained in the second string</returns>
        public static bool Contains(this string s, string otherString, bool caseSensitive)
        {
            if (caseSensitive)
            {
                return s.Contains(otherString);
            }
            else
            {
                return s.ToLower().Contains(otherString.ToLower());
            }
        }

        /// <summary>
        /// Returns true if the first string starts with the second string
        /// </summary>
        /// <param name="s"></param>
        /// <param name="otherString"></param>
        /// <param name="caseSensitive"></param>
        /// <returns>True if the first string starts with the second string</returns>
        public static bool StartsWith(this string s, string otherString, bool caseSensitive = false)
        {
            return s.StartsWith(otherString, !caseSensitive, null);
        }

        /// <summary>
        /// Returns true if the first string ends with the second string
        /// </summary>
        /// <param name="s"></param>
        /// <param name="otherString"></param>
        /// <param name="caseSensitive"></param>
        /// <returns>True if the first string ends with the second string</returns>
        public static bool EndsWith(this string s, string otherString, bool caseSensitive = false)
        {
            return s.EndsWith(otherString, !caseSensitive, null);
        }

        /// <summary>
        /// Returns true if the first string is greater than the second string
        /// </summary>
        /// <param name="s"></param>
        /// <param name="otherString"></param>
        /// <returns>True if the first string value is greater than the second string value</returns>
        public static bool IsGreaterThan(this string s, string otherString)
        {
            return string.Compare(s, otherString) > 0;
        }

        /// <summary>
        /// Returns true if the first string is greater than or equal the second string
        /// </summary>
        /// <param name="s"></param>
        /// <param name="otherString"></param>
        /// <param name="caseSensitive"></param>
        /// <returns>True if the value of first string is greater than or equal the second string value</returns>
        public static bool IsGreaterThanOrEqualTo(this string s, string otherString, bool caseSensitive = false)
        {
            return (s.IsGreaterThan(otherString) | s.IsEqualTo(otherString, caseSensitive));
        }

        /// <summary>
        /// Returns true if the first string is less than  the second string
        /// </summary>
        /// <param name="s"></param>
        /// <param name="otherString"></param>
        /// <returns>True if the vslue of first string is less than  the second string value</returns>
        public static bool IsLessThan(this string s, string otherString)
        {
            return string.Compare(s, otherString) < 0;
        }

        /// <summary>
        /// Returns true if the first string is less than or equal  the second string
        /// </summary>
        /// <param name="s"></param>
        /// <param name="otherString"></param>
        /// <param name="caseSensitive"></param>
        /// <returns>True if the first string value is less than or equal  the second string value</returns>
        public static bool IsLessThanOrEqualTo(this string s, string otherString, bool caseSensitive = false)
        {
            return (s.IsLessThan(otherString) | s.IsEqualTo(otherString, caseSensitive));
        }

        /// <summary>
        /// Returns true if the string is equal to a string in a list
        /// </summary>
        /// <param name="theString"></param>
        /// <param name="otherStrings"></param>
        /// <param name="caseSensitive"></param>
        /// <returns>True if the string is equal to a string in a list</returns>
        public static bool IsEqualToAnyOfTheFollowing(this string theString, IEnumerable<string> otherStrings, bool caseSensitive = false)
        {
            foreach (string otherString in otherStrings)
            {
                if(theString.IsEqualTo(otherString, caseSensitive))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Returns true if the string is contained to a string in a list
        /// </summary>
        /// <param name="theString"></param>
        /// <param name="otherStrings"></param>
        /// <param name="caseSensitive"></param>
        /// <returns>True if the string is contained to a string in a list</returns>
        public static bool ContainsAnyOfTheFollowing(this string theString, IEnumerable<string> otherStrings, bool caseSensitive = false)
        {
            foreach (string otherString in otherStrings)
            {
                if (theString.Contains(otherString))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns true if the string starts with any string in the list
        /// </summary>
        /// <param name="theString"></param>
        /// <param name="otherStrings"></param>
        /// <param name="caseSensitive"></param>
        /// <returns>True if the string starts with any string in the list</returns>
        public static bool StartsWithAnyOfTheFollowing(this string theString, IEnumerable<string> otherStrings, bool caseSensitive = false)
        {
            foreach (string otherString in otherStrings)
            {
                if (theString.StartsWith(otherString, caseSensitive))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Returns true if the string ends with any string in the list
        /// </summary>
        /// <param name="theString"></param>
        /// <param name="otherStrings"></param>
        /// <param name="caseSensitive"></param>
        /// <returns>True if the string ends with any string in the list</returns>
        public static bool EndsWithAnyOfTheFollowing(this string theString, IEnumerable<string> otherStrings, bool caseSensitive = false)
        {
            foreach (string otherString in otherStrings)
            {
                if (theString.EndsWith(otherString, caseSensitive))
                    return true;

            }
            return false;
        }

        /// <summary>
        /// Returns true if the string value is greater than any string in the list
        /// </summary>
        /// <param name="s"></param>
        /// <param name="otherStrings"></param>
        /// <returns>True if the string value is greater than any string in the list</returns>
        public static bool IsGreaterThanAnyOfTheFollowing(this string s, IEnumerable<string> otherStrings)
        {
            foreach (string otherString in otherStrings)
            {
                if (s.IsGreaterThan(otherString))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Returns true if the string value is grater than or equal any string value in the list
        /// </summary>
        /// <param name="s"></param>
        /// <param name="otherStrings"></param>
        /// <param name="caseSensitive"></param>
        /// <returns>True if the string value is grater than or equal any string value in the list</returns>
        public static bool IsGreaterThanOrEqualToAnyOfTheFollowing(this string s, IEnumerable<string> otherStrings, bool caseSensitive = false)
        {
            foreach (string otherString in otherStrings)
            {
                if (s.IsGreaterThanOrEqualTo(otherString, caseSensitive))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Returns true if the string value is less than any string value in the list
        /// </summary>
        /// <param name="s"></param>
        /// <param name="otherStrings"></param>
        /// <returns>True if the string value is less than any string value in the list</returns>
        public static bool IsLessThanAnyOfTheFollowing(this string s, IEnumerable<string> otherStrings)
        {
            foreach (string otherString in otherStrings)
            {
                if (s.IsLessThan(otherString))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Returns true if the string is less than or equal any string in the list
        /// </summary>
        /// <param name="s"></param>
        /// <param name="otherStrings"></param>
        /// <param name="caseSensitive"></param>
        /// <returns>Returns true if the string is less than or equal any string in the list</returns>
        public static bool IsLessThanOrEqualToAnyOfTheFollowing(this string s, IEnumerable<string> otherStrings, bool caseSensitive = false)
        {
            foreach (string otherString in otherStrings)
            {
                if (s.IsLessThanOrEqualTo(otherString, caseSensitive))
                    return true;
            }
            return false;
        }

        
        /// <summary>
        /// Return true if the string is numberic
        /// </summary>
        /// <param name="s"></param>
        /// <returns>True if the string is numeric</returns>
        public static bool IsANumber(this string s)
        {
            double result = 0;
            return Double.TryParse(s, out result);
        }

     

        /// <summary>
        /// Return true if the string is an integer
        /// </summary>
        /// <param name="s"></param>
        /// <returns>True if the string is an integer</returns>
        public static bool IsAnInteger(this string s)
        {
            int i;
            return int.TryParse(s, out i);
        }

        /// <summary>
        /// Return true if the string is a long integer
        /// </summary>
        /// <param name="s"></param>
        /// <returns>True if the string is a long integer</returns>
        public static bool IsALongInteger(this string s)
        {
            uint i;
            return uint.TryParse(s, out i);
        }

        /// <summary>
        /// Same as Long integer
        /// </summary>
        /// <param name="s"></param>
        /// <returns>True if it is an integer number</returns>
        public static bool IsAWholeNumber(this string s)
        {
            return IsALongInteger(s);
        }


        /// <summary>
        /// Return true if the string is decimal number
        /// </summary>
        /// <param name="s"></param>
        /// <returns>True if the string is decimal number</returns>
        public static bool IsADecimal(this string s)
        {
            decimal d;
            return decimal.TryParse(s, out d);
        }

        /// <summary>
        /// Return true if the string is an email address
        /// </summary>
        /// <param name="s"></param>
        /// <returns>True if the string is a valid email string</returns>
        public static bool IsAnEmailAddress(this string s)
        {
            try
            {
                MailAddress m = new MailAddress(s);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Return array of strings  in camel case from a string
        /// </summary>
        /// <param name="s"></param>
        /// <returns>Array of strings  in camel case</returns>
        public static string[] SplitCamelCase(this string s)
        {
            return System.Text.RegularExpressions.Regex.Split(s, @"(?<!(^|[A-Z0-9]))(?=[A-Z0-9])|(?<!^)(?=[A-Z][a-z])");
        }

        
        /// <summary>
        /// Remove characters Before given length
        /// </summary>
        /// <param name="s"></param>
        /// <param name="characterIndex"></param>
        /// <returns>Substing of input string</returns>
        public static string RemoveBefore(this string s, int characterIndex)
        {
            if (s.NzString().Length >= characterIndex)
            {
                s = s.Substring(characterIndex, s.Length - characterIndex);
            }
            return s;
        }

        /// <summary>
        /// Remove character Before given Last string
        /// </summary>
        /// <param name="s"></param>
        /// <param name="text"></param>
        /// <returns>Substing of input string</returns>
        public static string RemoveBeforeLast(this string s, string text)
        {
            if (s.Contains(text))
            {
                int characterIndex = s.Length - s.Reverse().IndexOf(text.Reverse());
                s = s.RemoveBefore(characterIndex);
            }
            return s;
        }

        /// <summary>
        /// Remove characters Before occurence of First given string
        /// </summary>
        /// <param name="s"></param>
        /// <param name="text"></param>
        /// <returns>Substing of input string</returns>
        public static string RemoveBeforeFirst(this string s, string text)
        {
            if (s.Contains(text))
            {
                int characterIndex = s.IndexOf(text) + 0;
                s = s.RemoveBefore(characterIndex);
            }
            return s;
        }

        /// <summary>
        /// Remove Last and After character giving (parameter) in a text
        /// </summary>
        /// <param name="s"></param>
        /// <param name="text"></param>
        /// <returns>>Substing of input string</returns>
        public static string RemoveLastAndAfter(this string s, string text)
        {
            if (s.Contains(text))
            {
                int characterIndex = s.LastIndexOf(text) + 1;
                s = s.RemoveAfter(characterIndex);
            }
            return s;

        }

        /// <summary>
        /// Remove characters from a string after giving a text (parameter) within the string
        /// </summary>
        /// <param name="s"></param>
        /// <param name="text"></param>
        /// <returns>Sudstring of  input string</returns>
        public static string RemoveAfterFirst(this string s, string text)
        {
            if (s.Contains(text))
            {
                int characterIndex = s.IndexOf(text) + text.Length+1;
                s = s.RemoveAfter(characterIndex);
            }
            return s;
        }

        /// <summary>
        /// Remove charachters from a string after giving a text (parameter) within the string
        /// </summary>
        /// <param name="s"></param>
        /// <param name="text"></param>
        /// <returns>Sudstring of  input string</returns>
        public static string RemoveFirstAndAfter(this string s, string text)
        {
            if (s.Contains(text))
            {
                int characterIndex = s.IndexOf(text) + 1;
                s = s.RemoveAfter(characterIndex);
            }
            return s;
        }

        /// <summary>
        /// Remove characters from a string after giving a length
        /// </summary>
        /// <param name="s"></param>
        /// <param name="characterIndex"></param>
        /// <returns>Sudstring of  input string</returns>
        public static string RemoveAfter(this string s, int characterIndex)
        {
            if (s.NzString().Length >= characterIndex)
            {
                s = s.Substring(0, characterIndex-1);
            }
            return s;
        }

        /// <summary>
        /// This method will return a string in reverse
        /// </summary>
        /// <param name="s"></param>
        /// <returns>string</returns>
        public static string Reverse(this string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }


        /// <summary>
        /// Create an array of string from a string
        /// </summary>
        /// <param name="str"></param>
        /// <param name="chunkSize"></param>
        /// <returns>Array for strings </returns>
        public static IEnumerable<string> Split(this string str, int chunkSize)
        {
            return Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize));
        }

        /// <summary>
        /// Sum Of Numbers in a string
        /// </summary>
        /// <param name="s"></param>
        /// <returns>Long integer</returns>
        public static long SumOfCharNumbers(this string s)
        {
            if (s == null)
            {
                return 0;
            }
            long total = 0;
            char[] cAry = s.ToCharArray();
            foreach (char c in cAry)
            {
                total += (int)c;
            }
            return total;
        }

        /// <summary>
        /// Remove the length in the data type. I.e varchar(12) become varchar
        /// </summary>
        /// <param name="s"></param>
        /// <param name="leftBracket"></param>
        /// <returns>String</returns>
        public static string RemoveDatatypeLength(this string s, char leftBracket = '(')
        {
            int leftBracketIndex = s.IndexOf(leftBracket);
            if (leftBracketIndex > 0)
            {
                return s.Substring(0, leftBracketIndex);
            }
            else
            {
                return s;
            }
        }


        /// <summary>
        /// Return an int exctracted from the begining of a string.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="throwException"></param>
        /// <returns>int</returns>
        public static int ExtractNumberFromBeginning(this string s, bool throwException = false)
        {
            if (s == null)
            {
                s = "";
            }
            string numberAsString = Regex.Match(s, @"\d+").Value;
            int i = 0;
            if (int.TryParse(numberAsString, out i))
            {
                return i;
            }
            else
            {
                if (throwException)
                {
                    throw new Exception("no number");
                }
                else
                {
                    return i;
                }
            }
        }

        /// <summary>
        /// Extract a number form string
        /// </summary>
        /// <param name="s"></param>
        /// <param name="leftBracket"></param>
        /// <param name="rightBracket"></param>
        /// <returns>int</returns>
        public static int ExtractNumberFromString(this string s, char leftBracket = '(', char rightBracket = ')')
        {
            int numberLength;
            string sNumber;
            int iNumber;
            int leftBracketIndex = s.IndexOf(leftBracket) + 1;
            int rightBracketIndex = s.IndexOf(rightBracket) + 1;


            numberLength = rightBracketIndex - leftBracketIndex - 1;
            if (numberLength > 0)
            {
                sNumber = s.Substring(leftBracketIndex, numberLength);
                int commaIndex = sNumber.IndexOf(',');
                if (commaIndex > 0)
                {
                    sNumber = s.Substring(0, commaIndex);
                }
            }
            else
            {
                sNumber = "";
            }

            try
            {
                iNumber = Convert.ToInt32(sNumber);
            }
            catch (Exception)
            {
                iNumber = 0;
            }

            return iNumber;
        }

        /// <summary>
        /// Return the last word for a given string
        /// </summary>
        /// <param name="text"></param>
        /// <param name="excludeNumerics"></param>
        /// <param name="delimeter"></param>
        /// <returns>String</returns>
        public static string LastWord(this string text, bool excludeNumerics = false, char delimeter = ' ')
        {
            string temp = text;
            string _lastWord = "";
            if (excludeNumerics)
            {
                temp = text.ReplaceCharacters("1234567890", delimeter.ToString());
                if (temp[temp.Length - 1] == delimeter)
                {
                    temp = temp.RemoveAfter(text.Length - 1);
                }
            }

            int _position = temp.LastIndexOf(delimeter);

            if (_position > -1)
            {
                _lastWord = temp.Substring(_position + 1);
            }
            else
            {
                _lastWord = temp;
            }
            return _lastWord;
        }

        /// <summary>
        /// Return the left of a given string
        /// </summary>
        /// <param name="s"></param>
        /// <param name="length"></param>
        /// <returns>String</returns>
        public static string Left(this string s, int length)
        {
            if (s != null)
            {
                int stringLength = s.Length;
                if (stringLength < length)
                {
                    return s;
                }
                else
                {
                    return s.Substring(0, length);
                }
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// Return the right of a given string
        /// </summary>
        /// <param name="s"></param>
        /// <param name="length"></param>
        /// <returns>String</returns>
        public static string Right(this string s, int length)
        {
            if (s != null)
            {
                int stringLength = s.Length;
                if (stringLength < length)
                {
                    return s;
                }
                else
                {
                    return s.Substring(s.Length - length, length);
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Creating a title for a given string.
        /// </summary>
        /// <param name="s"></param>
        /// <returns>String</returns>
        public static string ToTitleCase(this string s)
        {
            char[] chars = s.ToCharArray();
            StringBuilder sb = new StringBuilder();

            bool capitalizeNextLetter = true;
            foreach (char c in chars)
            {
                if (capitalizeNextLetter)
                {
                    sb.Append(c.ToString().ToUpper());
                }
                else
                {
                    sb.Append(c.ToString().ToLower());
                }
                capitalizeNextLetter = char.IsWhiteSpace(c);
            }
            return sb.ToString();

        }

 
        /// <summary>
        /// Removes all the text after the last index of either a space or hyphen
        /// </summary>
        /// <param name="logicalName"></param>
        /// <returns>String</returns>
        public static string ChopOffEnd(this string logicalName)
        {
            int length = logicalName.Length;
            string reversedLogicalName = logicalName.Reverse();
            int spacePosition = logicalName.LastIndexOf(' ');
            int hyphenPosition = logicalName.LastIndexOf('-');
            int newLength = 0;

            if (spacePosition == hyphenPosition)
            {
                newLength = 0;
            }

            if (spacePosition > hyphenPosition)
            {
                newLength = spacePosition;
            }

            if (hyphenPosition > spacePosition)
            {
                if (hyphenPosition.Equals(0))
                {
                    newLength = 0;
                }
                else
                {
                    newLength = hyphenPosition;
                }
            }
            logicalName = logicalName.Substring(0, newLength);
            return logicalName;
        }

        /// <summary>
        ///  Replace characters in a string wiht "_"
        /// </summary>
        /// <param name="s"></param>
        /// <param name="charsToReplace"></param>
        /// <param name="replacementString"></param>
        /// <returns>String</returns>
        public static string ReplaceCharacters(this string s, string charsToReplace, string replacementString = "_")
        {
            return ReplaceCharacters(s, charsToReplace.ToCharArray(), replacementString);
        }

        /// <summary>
        ///  Replace array of characters in a string with "_" 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="charsToReplace"></param>
        /// <param name="replacementString"></param>
        /// <returns>String</returns>
        public static string ReplaceCharacters(this string s, char[] charsToReplace, string replacementString = "_")
        {
            foreach (char c in charsToReplace)
            {
                s = s.Replace(c.ToString(), replacementString);
            }
            return s;
        }

        /// <summary>
        /// Repeat a string number of times
        /// </summary>
        /// <param name="s"></param>
        /// <param name="iterations"></param>
        /// <returns>String</returns>
        public static string Repeat(this string s, int iterations)
        {
            string newString = s;
            for (int i = 1; i < iterations; i++)
            {
                newString += s;
            }
            return newString;
        }
        
        /// <summary>
        ///  Replace first Occurance with given character
        /// </summary>
        /// <param name="s"></param>
        /// <param name="ToBeReplaced"></param>
        /// <param name="ReplaceWith"></param>
        /// <returns>String</returns>
        public static string ReplaceFirstOccurence(this string s, string ToBeReplaced, string ReplaceWith)
        {
            int indexOfToBeReplaced = s.IndexOf(ToBeReplaced);

            if (indexOfToBeReplaced < 0)
                return s;

            return s
                .Remove(indexOfToBeReplaced, ToBeReplaced.Length)
                .Insert(indexOfToBeReplaced, ReplaceWith);
        }

    }

}
