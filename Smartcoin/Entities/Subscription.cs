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
    public class Subscription : SmartcoinObject<Subscription>
    {
        public Subscription()
            : base("subscription")
        {

        }

        #region Properties

        /// <summary>
        /// Environment:
        /// False = Test, True = Production
        /// </summary>
        [JsonProperty("livemode")]
        public bool LiveMode { get; private set; }

        [JsonProperty("customer")]
        public string Customer { get; set; }

        [JsonProperty("plan")]
        public Plan Plan { get; set; }

        [JsonProperty("quantity")]
        public int? Quantity { get; set; }

        [JsonProperty("trial_end")]
        public int? TrialEnd { get; set; }

        [JsonProperty("status")]
        public string Status { get; private set; }

        [JsonProperty("cancel_at_period_end")]
        public bool CancelAtPeriodEnd { get; set; }

        [JsonProperty("start")]
        public int Start { get; private set; }

        [JsonProperty("ended_at")]
        public int? EndedAt { get; private set; }

        [JsonProperty("canceled_at")]
        public int? CanceledAt { get; private set; }

        [JsonProperty("current_period_start")]
        public int CurrentPeriodStart { get; private set; }

        [JsonProperty("current_period_end")]
        public int CurrentPeriodEnd { get; private set; }

        [JsonProperty("trial_start")]
        public int TrialStart { get; private set; }
        #endregion

        #region Methods
        public Subscription Create()
        {
            return Subscription.Create(this);
        }
        public Subscription Cancel(bool AtPeriodEnd = false)
        {
            return Subscription.Cancel(this);
        }
        public string ToCreate()
        {
            var stb = new StringBuilder();
            stb.AppendFormat("plan={0}", HttpUtility.UrlEncode(this.Plan.Id));
            if (this.TrialEnd != null) stb.AppendFormat("&trial_end={0}", HttpUtility.UrlEncode(this.TrialEnd.ToString()));
            if (this.Quantity != null) stb.AppendFormat("&quantity={0}", HttpUtility.UrlEncode(this.Quantity.ToString()));
            return stb.ToString();
        }
        public string ToCancel()
        {
            if (this.CancelAtPeriodEnd)
                return "at_period_end=true";
            return "";
        }
        #endregion

        #region Statics
        public static Subscription Get(string CustomerId, string SubscriptionId)
        {
            return FromJson(SmartcoinRequester.Get(SmartcoinContext.BASE_ENDPOINT + "/v1/customers/" + CustomerId + "/subscriptions/" + SubscriptionId));
        }
        public static Subscription Create(Subscription obj)
        {
            return FromJson(SmartcoinRequester.Post(SmartcoinContext.BASE_ENDPOINT + "/v1/customers/" + obj.Customer + "/subscriptions", obj.ToCreate()));
        }
        public static Subscription Cancel(Subscription obj)
        {
            return FromJson(SmartcoinRequester.Delete(SmartcoinContext.BASE_ENDPOINT + "/v1/customers/" + obj.Customer + "/subscriptions/" + obj.Id, obj.ToCancel()));
        }
        #endregion
    }
}
