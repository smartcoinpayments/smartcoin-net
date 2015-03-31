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
    public class Customer : SmartcoinObject<Customer>
    {
        #region Properties

        /// <summary>
        /// Environment:
        /// False = Test, True = Production
        /// </summary>
        [JsonProperty("livemode")]
        public bool LiveMode { get; private set; }

        /// <summary>
        /// Customer reference
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// Credit card information
        /// </summary>
        [JsonProperty("cards")]
        public SmartcoinList<Card> Cards { get; set; }

        [JsonProperty("created")]
        public int Created { get; private set; }

        [JsonProperty("default_card")]
        public string DefaultCard { get; private set; }

        #endregion

        public Customer()
            : base("customer")
        {

        }

        #region Methods

        public Customer Create()
        {
            return Customer.Create(this);
        }

        public Customer UpdateCard(string CardToken)
        {
            return Customer.UpdateCard(this, CardToken);
        }
        public Customer UpdateCard(Card card)
        {
            return Customer.UpdateCard(this, card);
        }

        public bool Delete()
        {
            return Customer.Delete(this);
        }

        public string ToCreate()
        {
            var stb = new StringBuilder();

            stb.AppendFormat("email={0}", HttpUtility.UrlEncode(this.Email));

            return stb.ToString();
        }

        #endregion

        #region Statics

        public static Customer Get(string idOrEmail)
        {
            var id = idOrEmail != null && idOrEmail.Contains("@") ? "?email=" + idOrEmail : idOrEmail;

            return FromJson(SmartcoinRequester.Get(SmartcoinContext.BASE_ENDPOINT + "/v1/customers/" + id));
        }

        public static SmartcoinList<Customer> ListAll()
        {
            return Serializer.FromJson<SmartcoinList<Customer>>(SmartcoinRequester.Get(SmartcoinContext.BASE_ENDPOINT + "/v1/customers"));
        }

        public static Customer Create(Customer obj)
        {
            return FromJson(SmartcoinRequester.Post(SmartcoinContext.BASE_ENDPOINT + "/v1/customers", obj.ToCreate()));
        }

        public static Customer UpdateCard(Customer obj, string CardToken)
        {
            var stb = new StringBuilder();
            stb.AppendFormat("card={0}", HttpUtility.UrlEncode(CardToken));
            return FromJson(SmartcoinRequester.Post(SmartcoinContext.BASE_ENDPOINT + "/v1/customers/" + obj.Id, stb.ToString()));
        }

        public static Customer UpdateCard(Customer obj, Card card)
        {
            var stb = new StringBuilder();
            card.ToCharge(stb);
            return FromJson(SmartcoinRequester.Post(SmartcoinContext.BASE_ENDPOINT + "/v1/customers/" + obj.Id, stb.ToString()));
        }

        public static bool Delete(Customer obj)
        {
            return Delete(obj.Id);
        }

        public static bool Delete(string id)
        {
            var response = SmartcoinDelete.FromJson(SmartcoinRequester.Delete(SmartcoinContext.BASE_ENDPOINT + "/v1/customers/" + id));
            return response.Deleted;
        }

        #endregion
    }
}
