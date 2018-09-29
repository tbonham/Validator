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
            return false;
        }
        public static Boolean FirstNameCustom(String strPatten, String strValue)
        {
            return false;
        }

        #endregion

        #region LastName

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
