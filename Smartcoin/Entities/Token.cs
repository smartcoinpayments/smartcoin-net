using Newtonsoft.Json;
using Smartcoin.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Smartcoin.Entities
{
    public class Token : SmartcoinObject<Token>
    {
        public Token()
            : base("token")
        {

        }

        #region Properties
        /// <summary>
        /// Environment:
        /// False = Test, True = Production
        /// </summary>
        [JsonProperty("livemode")]
        public bool LiveMode { get; private set; }

        [JsonProperty("created")]
        public int Created { get; private set; }

        [JsonProperty("used")]
        public bool Used { get; private set; }

        [JsonProperty("type")]
        public string Type { get; private set; }

        [JsonProperty("card")]
        public Card Card { get; set; }
        #endregion

        #region Methods
        public Token Create()
        {
            return Token.Create(this);
        }
        public string ToCreate()
        {
            var stb = new StringBuilder();

            stb.AppendFormat("number={0}", HttpUtility.UrlEncode(this.Card.Number.ToString()));
            stb.AppendFormat("&exp_month={0}", HttpUtility.UrlEncode(this.Card.ExpMonth.ToString()));
            stb.AppendFormat("&exp_year={0}", HttpUtility.UrlEncode(this.Card.ExpYear.ToString()));
            stb.AppendFormat("&cvc={0}", HttpUtility.UrlEncode(this.Card.Cvc.ToString()));
            stb.AppendFormat("&name={0}", HttpUtility.UrlEncode(this.Card.Name));
            if (!string.IsNullOrWhiteSpace(this.Card.Country)) stb.AppendFormat("&country={0}", HttpUtility.UrlEncode(this.Card.Country));
            if (!string.IsNullOrWhiteSpace(this.Card.AddressLine1)) stb.AppendFormat("&address_line_1={0}", HttpUtility.UrlEncode(this.Card.AddressLine1));
            if (!string.IsNullOrWhiteSpace(this.Card.AddressLine2)) stb.AppendFormat("&address_line2={0}", HttpUtility.UrlEncode(this.Card.AddressLine2));
            if (!string.IsNullOrWhiteSpace(this.Card.AddressDistrict)) stb.AppendFormat("&address_district={0}", HttpUtility.UrlEncode(this.Card.AddressDistrict));
            if (!string.IsNullOrWhiteSpace(this.Card.AddressCity)) stb.AppendFormat("&address_city={0}", HttpUtility.UrlEncode(this.Card.AddressCity));
            if (!string.IsNullOrWhiteSpace(this.Card.AddressState)) stb.AppendFormat("&address_state={0}", HttpUtility.UrlEncode(this.Card.AddressState));
            if (!string.IsNullOrWhiteSpace(this.Card.AddressCep)) stb.AppendFormat("&address_cep={0}", HttpUtility.UrlEncode(this.Card.AddressCep));
            if (!string.IsNullOrWhiteSpace(this.Card.AddressCountry)) stb.AppendFormat("&address_country={0}", HttpUtility.UrlEncode(this.Card.AddressCountry));

            return stb.ToString();
        }
        #endregion

        #region Statics
        public static Token Get(string Id)
        {
            return FromJson(SmartcoinRequester.Get(SmartcoinContext.BASE_ENDPOINT + "/v1/tokens/" + Id));
        }
        public static Token Create(Token obj)
        {
            return FromJson(SmartcoinRequester.Post(SmartcoinContext.BASE_ENDPOINT + "/v1/tokens", obj.ToCreate()));
        }
        #endregion
    }
}
