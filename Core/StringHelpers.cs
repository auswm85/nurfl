using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nurfl.Core
{
    public static class StringHelpers
    {
        //taken from robconery https://github.com/robconery/sugar/blob/master/StringValidations.cs
        private const string URL_REGEX = @"^^(https?\:\/\/)[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&%\$#_=]*)?$";

        public static bool IsValidUrl(this string url)
        {
            return Regex.IsMatch(url.Trim(), URL_REGEX, RegexOptions.IgnoreCase);
        }

        public static string AppendUrlParam(this string url, string key, string value)
        {
            if (!url.Contains("?"))
                return string.Format("{0}?{1}={2}", url, key, value);
            else
                return string.Format("{0}&{1}={2}", url, key, value);
        }
    }
}
