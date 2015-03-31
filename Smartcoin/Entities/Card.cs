using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Smartcoin.Entities
{
    public class Card : SmartcoinObject<Card>
    {
        public Card()
            : base("card")
        {

        }

        #region Properties

        [JsonProperty("last4")]
        public string Last4 { get; private set; }

        [JsonProperty("type")]
        public string Type { get; private set; }

        [JsonProperty("number")]
        public long Number { get; set; }

        [JsonProperty("exp_month")]
        public int ExpMonth { get; set; }

        [JsonProperty("exp_year")]
        public int ExpYear { get; set; }

        [JsonProperty("cvc")]
        public int Cvc { get; set; }

        [JsonProperty("fingerprint")]
        public string Fingerprint { get; private set; }

        [JsonProperty("Country")]
        public string Country { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("address_line1")]
        public string AddressLine1 { get; set; }

        [JsonProperty("address_line2")]
        public string AddressLine2 { get; set; }

        [JsonProperty("address_district")]
        public string AddressDistrict { get; set; }

        [JsonProperty("address_city")]
        public string AddressCity { get; set; }

        [JsonProperty("address_state")]
        public string AddressState { get; set; }

        [JsonProperty("address_cep")]
        public string AddressCep { get; set; }

        [JsonProperty("address_country")]
        public string AddressCountry { get; set; }
        #endregion

        #region Methods
        public void ToCharge(StringBuilder stb)
        {
            stb.AppendFormat("&card[number]={0}", HttpUtility.UrlEncode(this.Number.ToString()));
            stb.AppendFormat("&card[exp_month]={0}", HttpUtility.UrlEncode(this.ExpMonth.ToString()));
            stb.AppendFormat("&card[exp_year]={0}", HttpUtility.UrlEncode(this.ExpYear.ToString()));
            stb.AppendFormat("&card[cvc]={0}", HttpUtility.UrlEncode(this.Cvc.ToString()));
            stb.AppendFormat("&card[name]={0}", HttpUtility.UrlEncode(this.Name));
            if (!string.IsNullOrWhiteSpace(this.Country)) stb.AppendFormat("&card[country]={0}", HttpUtility.UrlEncode(this.Country));
            if (!string.IsNullOrWhiteSpace(this.AddressLine1)) stb.AppendFormat("&card[address_line1]={0}", HttpUtility.UrlEncode(this.AddressLine1));
            if (!string.IsNullOrWhiteSpace(this.AddressLine2)) stb.AppendFormat("&card[address_line2]={0}", HttpUtility.UrlEncode(this.AddressLine2));
            if (!string.IsNullOrWhiteSpace(this.AddressDistrict)) stb.AppendFormat("&card[address_district]={0}", HttpUtility.UrlEncode(this.AddressDistrict));
            if (!string.IsNullOrWhiteSpace(this.AddressCity)) stb.AppendFormat("&card[address_city]={0}", HttpUtility.UrlEncode(this.AddressCity));
            if (!string.IsNullOrWhiteSpace(this.AddressState)) stb.AppendFormat("&card[address_state]={0}", HttpUtility.UrlEncode(this.AddressState));
            if (!string.IsNullOrWhiteSpace(this.AddressCep)) stb.AppendFormat("&card[address_cep]={0}", HttpUtility.UrlEncode(this.AddressCep));
            if (!string.IsNullOrWhiteSpace(this.AddressCountry)) stb.AppendFormat("&card[address_country]={0}", HttpUtility.UrlEncode(this.AddressCountry));
        }
        #endregion
    }
}
