using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace DevTeamUtils.Api.Models
{
    [DataContract]
    public class IfxError : Base
    {
        [DataMember]
        [BsonId]
        public System.Guid Id { get; set; }

        [DataMember]
        public int Code { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Detail { get; set; }

        public void DeserializeJson(JObject json)
        {
            //Id = ((JValue)json.SelectToken("bairro")).Value.ToBson();
            Code = System.Convert.ToInt32(getTokenValue(json, "code"));
            Description = getTokenValue(json, "description");
            Detail = getTokenValue(json, "detail");
        }
    }
}
