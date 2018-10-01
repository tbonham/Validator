using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Validator
{
    public static class Validator
    {
        #region FirstName
        public static Boolean FirstName(String strValue)
        {
            const String strPatten = "[A-Za-z]{1,50}";
            Match match = Regex.Match(strValue, strPatten);                
            if(match.Success)
            {
                return true;
            }
            return false;
        }

        public static Boolean FirstNameProper(String strValue)
        {
            const String strPatten = "[A-Z]{1}[A-Za-z]{1,50}";
            Match match = Regex.Match(strValue, strPatten);
            if(match.Success)
            {
                return true;
            }
            return false;
        }
        public static Boolean FirstNameCustom(String strPatten, String strValue)
        {
            Match match = Regex.Match(strValue, strPatten);
            if(match.Success)
            {
                return true;
            }
            return false;
        }

        #endregion

        #region LastName
        public static Boolean LastName(String strValue)
        {
            const String strPatten = "[A-Za-z]{1,50}";
            Match match = Regex.Match(strValue, strPatten);
            if(match.Success)
            {
                return true;
            }
            return false;
        }
        public static Boolean LastNameProper(String strValue)
        {
            const String strPatten = "[A-Z]{1}[A-Za-z]{1,50}";
            Match match = Regex.Match(strValue, strPatten);
            if(match.Success)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Custom validation for last names. 
        /// strPatten well hold custom regex. 
        /// strValue well be the value.
        /// </summary>
        /// <param name="strPatten"></param>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static Boolean LastNameCustom(String strPatten,String strValue)
        {
            Match match = Regex.Match(strValue, strPatten);
            if (match.Success)
            {
                return true;
            }
            return false;
        }
        #endregion

        /// <summary>
        /// SanitizeGeneralInput is a general function that will remove most degest items from a string.
        /// It can also remove good things.
        /// So Make sure you test everything well.
        /// </summary>
        public static String SanitizeGeneralInput(String value)
        {
            const String strBlank = @"";
            const double TimeOut = 4.0;
            const String strRegex = @"[^\w\.@-]";
            try
            {
                return Regex.Replace(value, strRegex,
                    strBlank,
                    RegexOptions.None,
                    TimeSpan.FromSeconds(TimeOut));
            }
            catch (RegexMatchTimeoutException)
            {
                return String.Empty;
            }
        }
    }
}
