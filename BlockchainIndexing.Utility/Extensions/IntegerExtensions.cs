using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainIndexing.Utility.Extensions
{
    public static class IntegerExtensions
    {
        public static string ToHexString(this int value)
        {
            return string.Format("0x{0:X}", value);
        }
    }
}