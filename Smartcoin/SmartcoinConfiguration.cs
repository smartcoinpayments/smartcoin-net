
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartcoin
{
    public static class SmartcoinConfiguration
    {
        public static string ApiKey { get; set; }
        public static string ApiSecret { get; set; }

        public static void SetKeys(string key, string secret)
        {
            ApiKey = key;
            ApiSecret = secret;
        }
    }
}
