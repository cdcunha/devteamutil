using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace DevTeamUtils.Api.Models
{
    public class User : BaseModel
    {
        [BsonConstructor]
        public User() : base() { }

        [DataMember]
        public string Nome { get; set; }
        [DataMember]
        public string Apelido { get; set; }
        [DataMember]
        public string Senha { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        [BsonRepresentation(BsonType.Boolean)]
        public bool Online { get; set; }
        [DataMember]
        public string ConnectionId { get; set; }

        public void DeserializeJson(JObject json)
        {
            //Id = ((JValue)json.SelectToken("bairro")).Value.ToBson();
            Nome = getTokenValue(json, "nome");
            Apelido = getTokenValue(json, "apelido");
            Senha = getTokenValue(json, "senha");
            Email = getTokenValue(json, "email");

            //Exemplo para pegar boolean
            /*
            if (!string.IsNullOrEmpty(getTokenValue(json, "ativo")))
                Ativo = Convert.ToBoolean(getTokenValue(json, "ativo"));
            */
        }
    }
}
