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
    public class Charge : SmartcoinObject<Charge>
    {
        public Charge()
            : base("charge")
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

        [JsonProperty("paid")]
        public bool Paid { get; private set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("refunded")]
        public bool Refunded { get; private set; }

        [JsonProperty("card")]
        public Card Card { get; set; }

        public string CardToken { get; set; }

        [JsonProperty("customer")]
        public string Customer { get; set; }

        [JsonProperty("bank_slip")]
        public BankSlip BankSlip { get; private set; }

        [JsonProperty("captured")]
        public bool? Captured { get; private set; }

        [JsonProperty("capture")]
        public bool Capture { get; set; }

        [JsonProperty("refunds")]
        public List<Refund> Refunds { get; private set; }

        [JsonProperty("fees")]
        public List<Fee> Fees { get; private set; }

        [JsonProperty("installment")]
        public int Installment { get; set; }

        [JsonProperty("installments")]
        public List<Installment> Installments { get; private set; }

        [JsonProperty("failure_message")]
        public string FailureMessage { get; private set; }

        [JsonProperty("failure_code")]
        public string FailureCode { get; private set; }

        [JsonProperty("amount_refunded")]
        public object AmountRefunded { get; private set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("receipt_email")]
        public string ReceiptEmail { get; set; }

        [JsonProperty("shipment")]
        public Shipment Shipment { get; set; }
        #endregion


        #region Methods
        public Charge Create()
        {
            return Charge.Create(this);
        }

        public Charge Refund()
        {
            return Charge.Refund(this);
        }

        public Charge CaptureNow()
        {
            return Charge.CaptureNow(this);
        }

        public Charge UpdateDescription()
        {
            return Charge.UpdateDescription(this);
        }



        public string ToCreate()
        {
            var stb = new StringBuilder();

            stb.AppendFormat("amount={0}", HttpUtility.UrlEncode(this.Amount.ToString()));
            stb.AppendFormat("&currency={0}", HttpUtility.UrlEncode(this.Currency));

            if (this.Type.ToLower().Equals("credit_card") || string.IsNullOrWhiteSpace(this.Type))
            {
                stb.Append("&type=credit_card");
                if (this.Card != null)
                    this.Card.ToCharge(stb);
                else if (!string.IsNullOrWhiteSpace(this.CardToken))
                    stb.AppendFormat("&card={0}", HttpUtility.UrlEncode(this.CardToken));
                else
                    stb.AppendFormat("&customer={0}", HttpUtility.UrlEncode(this.Customer));

                if (Installment > 0)
                    stb.AppendFormat("&installment={0}", HttpUtility.UrlEncode(this.Installment.ToString()));
            }
            else
                stb.AppendFormat("&type={0}", HttpUtility.UrlEncode(this.Type));
            stb.AppendFormat("&capture={0}", HttpUtility.UrlEncode(this.Capture.ToString().ToLower()));
            if (!string.IsNullOrWhiteSpace(this.Description)) stb.AppendFormat("&description={0}", HttpUtility.UrlEncode(this.Description));
            if (!string.IsNullOrWhiteSpace(this.Reference)) stb.AppendFormat("&reference={0}", HttpUtility.UrlEncode(this.Reference));
            if (!string.IsNullOrWhiteSpace(this.ReceiptEmail)) stb.AppendFormat("&receipt_email={0}", HttpUtility.UrlEncode(this.ReceiptEmail));
            if (this.Shipment != null) this.Shipment.ToCharge(stb);

            return stb.ToString();
        }
        public string ToUpdateDescription()
        {
            var stb = new StringBuilder();
            stb.AppendFormat("description={0}", HttpUtility.UrlEncode(this.Description));
            return stb.ToString();
        }
        public string ToRefundOfCapture()
        {
            var stb = new StringBuilder();
            stb.AppendFormat("amount={0}", HttpUtility.UrlEncode(this.Amount.ToString()));
            return stb.ToString();
        }
        #endregion


        #region Statics
        public static Charge Get(string idOrEmail)
        {
            var id = idOrEmail != null && idOrEmail.Contains("@") ? "?email=" + idOrEmail : idOrEmail;

            return FromJson(SmartcoinRequester.Get(SmartcoinContext.BASE_ENDPOINT + "/v1/charges/" + id));
        }

        public static SmartcoinList<Charge> ListAll(int? count = null, int? offset = null, string created_operator = null, DateTime? created = null)
        {
            StringBuilder stb = new StringBuilder();
            var init = "?";
            if (count != null)
            {
                stb.AppendFormat("{0}count={1}", init, count.ToString());
                init = "&";
            }
            if (offset != null)
            {
                stb.AppendFormat("{0}offset={1}", init, offset.ToString());
                init = "&";
            }
            if (created != null)
            {
                var op = "gte";
                switch(created_operator){
                    case ">":
                        op = "gt";
                        break;
                    case "<":
                        op = "lt";
                        break;
                    case "<=":
                        op = "lte";
                        break;
                }


                stb.AppendFormat("{0}created={1}:{2}", init, op, created.Value.ToString("yyyy-MM-dd"));
                init = "&";
            }

            return Serializer.FromJson<SmartcoinList<Charge>>(SmartcoinRequester.Get(SmartcoinContext.BASE_ENDPOINT + "/v1/charges" + stb.ToString()));
        }

        public static Charge Create(Charge obj)
        {
            return FromJson(SmartcoinRequester.Post(SmartcoinContext.BASE_ENDPOINT + "/v1/charges", obj.ToCreate()));
        }

        public static Charge UpdateDescription(Charge obj)
        {
            return FromJson(SmartcoinRequester.Post(SmartcoinContext.BASE_ENDPOINT + "/v1/charges/" + obj.Id, obj.ToUpdateDescription()));
        }

        public static Charge Refund(Charge obj)
        {
            return FromJson(SmartcoinRequester.Post(SmartcoinContext.BASE_ENDPOINT + "/v1/charges/" + obj.Id + "/refund", obj.ToRefundOfCapture()));
        }

        public static Charge CaptureNow(Charge obj)
        {
            return FromJson(SmartcoinRequester.Post(SmartcoinContext.BASE_ENDPOINT + "/v1/charges/" + obj.Id + "/capture", obj.ToRefundOfCapture()));
        }

        #endregion
    }
}
