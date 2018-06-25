using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace DevTeamUtils.Api.Models
{
    [DataContract]
    public class Tabela : BaseModel
    {
        [DataMember]
        public System.Guid ProjectId { get; set; }

        [DataMember]
        public string Nome { get; set; }

        [DataMember]
        public string Descricao { get; set; }

        [DataMember]
        public string TipoScrypt { get; set; } //C=Create, I=Insert, U=Update, A=Alter, O=Other

        [DataMember]
        public string Mnemonico { get; set; }

        [DataMember]
        public string Script { get; set; }

        [DataMember]
        public bool Validated { get; set; }

        public Tabela() : base() { }

        public void DeserializeJson(JObject json)
        {
            //Id = ((JValue)json.SelectToken("bairro")).Value.ToBson();
            Nome = getTokenValue(json, "nome");
            Descricao = getTokenValue(json, "descricao");
            TipoScrypt = getTokenValue(json, "tipoScrypt");
            Mnemonico = getTokenValue(json, "mnemonico");
            Script = getTokenValue(json, "script");

            if (!string.IsNullOrEmpty(getTokenValue(json, "validated")))
                Validated = System.Convert.ToBoolean(getTokenValue(json, "validated"));
        }
    }
}
