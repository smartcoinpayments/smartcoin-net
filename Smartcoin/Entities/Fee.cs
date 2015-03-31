using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartcoin.Entities
{
    public class Fee : SmartcoinObject<Fee>
    {
        public Fee()
            : base("fee")
        {

        }

        [JsonProperty("amount")]
        public int Amount { get; private set; }

        [JsonProperty("percentage")]
        public int Percentage { get; private set; }

        [JsonProperty("currency")]
        public string Currency { get; private set; }

        [JsonProperty("type")]
        public string Type { get; private set; }
    }
}
