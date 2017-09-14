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
    public class ConexaoSishosp : BaseModel
    {   
        [DataMember]
        public string Tipo { get; set; }

        /// <summary>
        /// NomeServidor para Informix e Service Name para Oracle
        /// </summary>
        [DataMember]
        public string NomeServidor { get; set; }

        /// <summary>
        /// IP para Informix e Host para Oracle
        /// </summary>
        [DataMember]
        public string Ip { get; set; }

        [DataMember]
        [BsonRepresentation(BsonType.Int32)]
        public int Porta { get; set; }

        [DataMember]
        public string Usuario { get; set; }

        [DataMember]
        public string Senha { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        [BsonDateTimeOptions(DateOnly = true, Kind = System.DateTimeKind.Local)]
        public System.DateTime? DataStatus { get; set; }

        [BsonConstructor]
        public ConexaoSishosp() : base() { }
        
        public void DeserializeJson(JObject json)
        {
            //Id = ((JValue)json.SelectToken("bairro")).Value.ToBson();
            Tipo = getTokenValue(json, "tipo");
            NomeServidor = getTokenValue(json, "nomeServidor");
            Ip = getTokenValue(json, "ip");
            Porta = int.Parse(getTokenValue(json, "porta"));
            Usuario = getTokenValue(json, "usuario");
            Senha = getTokenValue(json, "senha");
            Status = getTokenValue(json, "status");
            DataStatus = TokenToDateTime(json, "dataStatus", "dd-MM-yyyy");

            /*Exemplo de campo boolean
             * if (!string.IsNullOrEmpty(getTokenValue(json, "ativo")))
                Ativo = Convert.ToBoolean(getTokenValue(json, "ativo"));
            */
        }
    }
}
