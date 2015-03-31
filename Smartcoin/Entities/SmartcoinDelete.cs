using Newtonsoft.Json;
using Smartcoin.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartcoin.Entities
{
    public class SmartcoinDelete
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        public static SmartcoinDelete FromJson(string json)
        {
            return Serializer.FromJson<SmartcoinDelete>(json);
        }
    }
}
