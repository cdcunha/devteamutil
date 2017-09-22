using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace DevTeamUtils.Api.Models
{
    [DataContract]
    public class Contato : BaseModel
    {
        [DataMember]
        public string Nome { get; set; }

        [DataMember]
        public string Telefone { get; set; }

        [DataMember]
        public string Cargo { get; set; }

        [DataMember]
        public string Local { get; set; }

        [DataMember]
        public string Observacao { get; set; }

        [BsonConstructor]
        public Contato() : base() { }

        //[BsonConstructor]
        //public Contato(string nome) : base(nome){ }
        
        public void DeserializeJson(JObject json)
        {
            //Id = ((JValue)json.SelectToken("bairro")).Value.ToBson();
            Nome = getTokenValue(json, "nome");
            Telefone = getTokenValue(json, "telefone");
            Cargo = getTokenValue(json, "cargo");
            Local = getTokenValue(json, "local");
            Observacao = getTokenValue(json, "observacao");

            //Exemplo para pegar boolean
            /*
            if (!string.IsNullOrEmpty(getTokenValue(json, "ativo")))
                Ativo = Convert.ToBoolean(getTokenValue(json, "ativo"));
            */
        }
    }
}
