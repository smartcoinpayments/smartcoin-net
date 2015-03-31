using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartcoinTest
{
    public static class TestUtils
    {
        public static string RandomString(string basestr = null)
        {
            var chars = "abcdefghijklmnopqrstuvwxyz";
            if (basestr != null)
                chars = basestr;
            var str = "";
            var r = new Random();
            for (var i = 0; i < 10; i++)
            {
                str += chars[r.Next(0, chars.Length - 1)];
            }
            return str;
        }
    }
}
