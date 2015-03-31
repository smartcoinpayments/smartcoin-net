using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Smartcoin.Entities
{
    public class Shipment : SmartcoinObject<Shipment>
    {
        public Shipment()
            : base("shipment")
        {

        }

        #region Properties

        [JsonProperty("amount")]
        public int? Amount { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("address_line_1")]
        public string AddressLine1 { get; private set; }

        [JsonProperty("address_line_2")]
        public string AddressLine2 { get; private set; }

        [JsonProperty("address_district")]
        public string AddressDistrict { get; private set; }

        [JsonProperty("address_city")]
        public string AddressCity { get; private set; }

        [JsonProperty("address_state")]
        public string AddressState { get; private set; }

        [JsonProperty("address_cep")]
        public string AddressCep { get; private set; }

        [JsonProperty("address_country")]
        public string AddressCountry { get; private set; }

        #endregion

        #region Methods
        public void ToCharge(StringBuilder stb)
        {
            if (this.Amount != null) stb.AppendFormat("&shipment[amount]={0}", HttpUtility.UrlEncode(this.Amount.ToString()));
            if (!string.IsNullOrWhiteSpace(this.Name)) stb.AppendFormat("&shipment[name]={0}", HttpUtility.UrlEncode(this.Name));
            if (!string.IsNullOrWhiteSpace(this.Phone)) stb.AppendFormat("&shipment[phone]={0}", HttpUtility.UrlEncode(this.Phone));
            if (!string.IsNullOrWhiteSpace(this.AddressLine1)) stb.AppendFormat("&shipment[address_line1]={0}", HttpUtility.UrlEncode(this.AddressLine1));
            if (!string.IsNullOrWhiteSpace(this.AddressLine2)) stb.AppendFormat("&shipment[address_line2]={0}", HttpUtility.UrlEncode(this.AddressLine2));
            if (!string.IsNullOrWhiteSpace(this.AddressDistrict)) stb.AppendFormat("&shipment[address_district]={0}", HttpUtility.UrlEncode(this.AddressDistrict));
            if (!string.IsNullOrWhiteSpace(this.AddressCity)) stb.AppendFormat("&shipment[address_city]={0}", HttpUtility.UrlEncode(this.AddressCity));
            if (!string.IsNullOrWhiteSpace(this.AddressState)) stb.AppendFormat("&shipment[address_state]={0}", HttpUtility.UrlEncode(this.AddressState));
            if (!string.IsNullOrWhiteSpace(this.AddressCep)) stb.AppendFormat("&shipment[address_cep]={0}", HttpUtility.UrlEncode(this.AddressCep));
            if (!string.IsNullOrWhiteSpace(this.AddressCountry)) stb.AppendFormat("&shipment[address_country]={0}", HttpUtility.UrlEncode(this.AddressCountry));
        }
        #endregion
    }
}
