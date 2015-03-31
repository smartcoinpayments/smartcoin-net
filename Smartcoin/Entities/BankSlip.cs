using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartcoin.Entities
{
    public class BankSlip : SmartcoinObject<BankSlip>
    {
        public BankSlip() :
            base("bank_slip")
        {

        }


        [JsonProperty("bar_code")]
        public string BarCode { get; private set; }

        [JsonProperty("link")]
        public string Link { get; private set; }

    }
}
