﻿using System;
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
            String strRegex = @"[^\w\.@-]";
            try
            {
                return Regex.Replace(value, strRegex,
                    "",
                    RegexOptions.None,
                    TimeSpan.FromSeconds(3.0));
            }
            catch (RegexMatchTimeoutException)
            {
                return String.Empty;
            }
        }
    }
}