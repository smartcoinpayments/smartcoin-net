using Newtonsoft.Json;
using Smartcoin.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartcoin.Entities
{
    public abstract class SmartcoinObject<T>
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("object")]
        public string Object { get; private set; }

        public SmartcoinObject(string _object)
        {
            this.Object = _object;
        }

        public virtual string ToJson()
        {
            return Serializer.ToJson(this);
        }

        public static T FromJson(string json)
        {
            return Serializer.FromJson<T>(json);
        }
    }
}
