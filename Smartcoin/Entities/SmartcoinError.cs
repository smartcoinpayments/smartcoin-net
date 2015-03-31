using Newtonsoft.Json;
using Smartcoin.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartcoin.Entities
{
    public class SmartcoinError
    {
        [JsonProperty("error")]
        public SmartcoinErrorResult Error { get; set; }

        public static SmartcoinError FromJson(string json)
        {
            return Serializer.FromJson<SmartcoinError>(json);
        }

        public class SmartcoinErrorResult
        {
            [JsonProperty("message")]
            public string Message { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }
        }
    }
}
