using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Route256_11082024
{
    internal static class StringHelper
    {
        public static string GetSeparator(this string input) 
        {
            string separator = input.Contains("\r\n") ? "\r\n" : input.Contains("\n") ? "\n" : "\r";
            return separator ?? string.Empty;
        }
    }
}
