using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartcoin.Entities
{
    public class SmartcoinList<T>
    {
        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("count")]
        public int TotalCount { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("data")]
        public List<T> Data { get; set; }
    }
}
