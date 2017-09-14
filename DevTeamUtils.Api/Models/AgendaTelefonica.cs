using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace DevTeamUtils.Api.Models
{
    [DataContract]
    public class AgendaTelefonica : BaseModel
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
        public AgendaTelefonica() : base() { }

        //[BsonConstructor]
        //public AgendaTelefonica(string nome) : base(nome){ }
        
        public void DeserializeJson(JObject json)
        {
            //Id = ((JValue)json.SelectToken("bairro")).Value.ToBson();
            Nome = getTokenValue(json, "nome");
            Telefone = getTokenValue(json, "nome");
            Cargo = getTokenValue(json, "nome");
            Local = getTokenValue(json, "nome");
            Observacao = getTokenValue(json, "nome");

            //Exemplo para pegar boolean
            /*
            if (!string.IsNullOrEmpty(getTokenValue(json, "ativo")))
                Ativo = Convert.ToBoolean(getTokenValue(json, "ativo"));
            */
        }
    }
}
