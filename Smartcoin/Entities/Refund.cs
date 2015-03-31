using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartcoin.Entities
{
    public class Refund : SmartcoinObject<Refund>
    {
        public Refund()
            : base("refund")
        {

        }

        [JsonProperty("amount")]
        public int Amount { get; private set; }

        [JsonProperty("created")]
        public int Created { get; private set; }

        [JsonProperty("currency")]
        public string Currency { get; private set; }
    }
}
