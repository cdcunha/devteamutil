using DevTeamUtils.Api.Enums;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DevTeamUtils.Api.Models
{
    [DataContract]
    public class Script : BaseModel
    {
        [DataMember]
        public System.Guid PassoId { get; set; }

        [DataMember]
        public string NomeScript { get; set; }

        [DataMember]
        public string DescricaoScript { get; set; }

        [DataMember]
        public EnumTipoScript TipoScript { get; set; }
        
        [DataMember]
        public string Mnemonico { get; set; }

        [DataMember]
        public string TxtScript { get; set; }

        [DataMember]
        public bool Validado { get; set; }

        public Script() : base()
        {
            List<Campo> Campos = new List<Campo>();
        }

        public bool ShowFields { get { return false; } private set { } }

        public void DeserializeJson(JObject json)
        {
            //Id = ((JValue)json.SelectToken("bairro")).Value.ToBson();
            NomeScript = getTokenValue(json, "nomeScript");
            DescricaoScript = getTokenValue(json, "descricaoScript");
            TipoScript = (EnumTipoScript)System.Enum.Parse(typeof(EnumTipoScript), getTokenValue(json, "tipoScript"));
            Mnemonico = getTokenValue(json, "mnemonico");
            TxtScript = getTokenValue(json, "script");

            if (!string.IsNullOrEmpty(getTokenValue(json, "validado")))
                Validado = System.Convert.ToBoolean(getTokenValue(json, "validado"));
        }
    }
}
