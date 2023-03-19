using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainIndexing.Utility.Extensions
{
    public static class StringExtensions
    {
        public static decimal HexToDecimal(this string str)
        {
            return Convert.ToDecimal(str.HexToInt64());
        }

        public static int HexToInt32(this string str)
        {
            return Convert.ToInt32(str, 16);
        }

        public static long HexToInt64(this string str)
        {
            return Convert.ToInt64(str, 16);
        }
    }
}