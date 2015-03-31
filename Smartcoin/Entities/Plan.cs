using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Smartcoin.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Smartcoin.Entities
{
    public class Plan : SmartcoinObject<Plan>
    {
        public Plan()
            : base("plan")
        {

        }


        #region Properties

        /// <summary>
        /// Environment:
        /// False = Test, True = Production
        /// </summary>
        [JsonProperty("livemode")]
        public bool LiveMode { get; private set; }

        [JsonProperty("interval")]
        public string Interval { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("created")]
        public int Created { get; private set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("interval_count")]
        public int? IntervalCount { get; set; }

        [JsonProperty("trial_period_days")]
        public int? TrialPeriodDays { get; set; }

        #endregion

        #region Methods
        public Plan Create()
        {
            return Plan.Create(this);
        }
        public Plan Update()
        {
            return Plan.Update(this);
        }
        public string ToCreate()
        {
            var stb = new StringBuilder();
            stb.AppendFormat("id={0}", HttpUtility.UrlEncode(this.Id));
            stb.AppendFormat("&amount={0}", HttpUtility.UrlEncode(this.Amount.ToString()));
            stb.AppendFormat("&currency={0}", HttpUtility.UrlEncode(this.Currency));
            stb.AppendFormat("&interval={0}", HttpUtility.UrlEncode(this.Interval));
            stb.AppendFormat("&name={0}", HttpUtility.UrlEncode(this.Name));
            if (this.IntervalCount != null) stb.AppendFormat("&interval_count={0}", HttpUtility.UrlEncode(this.IntervalCount.ToString()));
            if (this.TrialPeriodDays != null) stb.AppendFormat("&trial_period_days={0}", HttpUtility.UrlEncode(this.TrialPeriodDays.ToString()));
            return stb.ToString();
        }
        public string ToUpdate()
        {
            var stb = new StringBuilder();
            stb.AppendFormat("name={0}", HttpUtility.UrlEncode(this.Name));
            return stb.ToString();
        }
        #endregion

        #region Statics

        public static Plan Get(string Id)
        {
            return FromJson(SmartcoinRequester.Get(SmartcoinContext.BASE_ENDPOINT + "/v1/plans/" + Id));
        }

        public static SmartcoinList<Plan> ListAll()
        {
            return Serializer.FromJson<SmartcoinList<Plan>>(SmartcoinRequester.Get(SmartcoinContext.BASE_ENDPOINT + "/v1/plans"));
        }
        public static Plan Create(Plan obj)
        {
            return FromJson(SmartcoinRequester.Post(SmartcoinContext.BASE_ENDPOINT + "/v1/plans", obj.ToCreate()));
        }

        public static Plan Update(Plan obj)
        {
            return FromJson(SmartcoinRequester.Post(SmartcoinContext.BASE_ENDPOINT + "/v1/plans/" + obj.Id, obj.ToUpdate()));
        }

        public static bool Delete(string Id)
        {
            var r = SmartcoinRequester.Delete(SmartcoinContext.BASE_ENDPOINT + "/v1/plans/" + Id);
            JObject jobj = (JObject)JsonConvert.DeserializeObject(r);
            return jobj["deleted"].Value<bool>();
        }
        #endregion
    }
}
