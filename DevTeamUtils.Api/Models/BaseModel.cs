using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace DevTeamUtils.Api.Models
{
    [DataContract]
    public class BaseModel : Base
    {
        [DataMember]
        [BsonId(IdGenerator = typeof(GuidGenerator))]
        public System.Guid Id { get; set; }
        
        [DataMember]
        [BsonRepresentation(BsonType.Boolean)]
        public bool Ativo { get; set; }

        [DataMember]
        [BsonDateTimeOptions(Kind = System.DateTimeKind.Local)]
        public System.DateTime DataCriacao { get; private set; }

        [DataMember]
        [BsonDateTimeOptions(Kind = System.DateTimeKind.Local)]
        public System.DateTime? DataAlteracao { get; private set; }
        
        [BsonConstructor]
        public BaseModel() : base()
        {
            Ativo = true;
            DataCriacao = System.DateTime.Now;
        }

        public void SetDataAlteracao()
        {
            DataAlteracao = System.DateTime.Now;
        }
        
        protected DateTime? TokenToDateTime(JObject json, string key, string format)
        {
            string strDate = getTokenValue(json, key);
            if (!string.IsNullOrEmpty(strDate))
            {
                if (format.Length == 10)
                    strDate = strDate.Substring(0, 10);

                if (strDate.Contains("/"))
                    strDate = strDate.Replace('/', '-');

                return DateTime.ParseExact(strDate, format, CultureInfo.InvariantCulture);
            }
            else
            {
                return null;
            }
        }
    }
}
