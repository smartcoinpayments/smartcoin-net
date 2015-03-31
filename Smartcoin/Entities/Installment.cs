using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartcoin.Entities
{
    public class Installment : SmartcoinObject<Installment>
    {
        public Installment()
            : base("installment")
        {

        }

        [JsonProperty("amount")]
        public int Amount { get; private set; }

        [JsonProperty("paid")]
        public bool Paid { get; private set; }

        [JsonProperty("pay_day")]
        public int PayDay { get; private set; }

    }
}
