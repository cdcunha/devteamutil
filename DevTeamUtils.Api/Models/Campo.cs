using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace DevTeamUtils.Api.Models
{
    [DataContract]
    public class Campo : BaseModel
    {
        [DataMember]
        public System.Guid TabelaId { get; set; }

        [DataMember]
        public string Nome { get; set; }

        [DataMember]
        public string Descricao { get; set; }

        [DataMember]
        public string Tipo { get; set; } //String, Date, Integer, Blob

        [DataMember]
        public int Tamanho { get; set; }

        [DataMember]
        public string Valor { get; set; }

        [DataMember]
        public bool NotNull { get; set; }

        [DataMember]
        public string Constraint { get; set; } //PK, FK, Default

        [DataMember]
        public string MnemonicoRefFk { get; set; }

        [DataMember]
        public string FieldRefFk { get; set; }

        public void DeserializeJson(JObject json)
        {
            //Id = ((JValue)json.SelectToken("bairro")).Value.ToBson();
            Nome = getTokenValue(json, "nome");
            Descricao = getTokenValue(json, "descricao");
            //TypeScrypt = getTokenValue(json, "typeScrypt");
            //Mnemonico = getTokenValue(json, "mnemonico");
            //Script = getTokenValue(json, "script");
        }
    }
}
