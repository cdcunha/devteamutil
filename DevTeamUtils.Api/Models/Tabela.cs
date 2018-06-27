using DevTeamUtils.Api.Enums;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DevTeamUtils.Api.Models
{
    [DataContract]
    public class Tabela : Base
    {
        [DataMember]
        public string NomeTabela { get; set; }

        [DataMember]
        public string DescricaoTabela { get; set; }

        [DataMember]
        public EnumTipoScript TipoScript { get; set; }
        
        [DataMember]
        public string Mnemonico { get; set; }

        [DataMember]
        public string Script { get; set; }

        [DataMember]
        public bool Validado { get; set; }

        [DataMember]
        public List<Campo> Campos { get; private set; }

        public Tabela() : base()
        {
            List<Campo> Campos = new List<Campo>();
        }

        public void DeserializeJson(JObject json)
        {
            //Id = ((JValue)json.SelectToken("bairro")).Value.ToBson();
            NomeTabela = getTokenValue(json, "nomeTabela");
            DescricaoTabela = getTokenValue(json, "descricaoTabela");
            TipoScript = (EnumTipoScript)System.Enum.Parse(typeof(EnumTipoScript), getTokenValue(json, "tipoScript"));
            Mnemonico = getTokenValue(json, "mnemonico");
            Script = getTokenValue(json, "script");

            if (!string.IsNullOrEmpty(getTokenValue(json, "validado")))
                Validado = System.Convert.ToBoolean(getTokenValue(json, "validado"));
        }
    }
}
