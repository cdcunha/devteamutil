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
        public TipoScript TipoScript { get; set; }

        [DataMember]
        public TipoObjeto TipoObjeto { get; set; }
        
        [DataMember]
        public string Mnemonico { get; set; }

        [DataMember]
        public string NomeTabelaPai { get; set; }

        [DataMember]
        public string TxtScript { get; set; }

        [DataMember]
        public bool Validado { get; set; }

        public Script() : base(){ }

        public void DeserializeJson(JObject json)
        {
            //Id = ((JValue)json.SelectToken("bairro")).Value.ToBson();
            PassoId = json.Property("passoId").Value.ToObject<System.Guid>();
            NomeScript = getTokenValue(json, "nomeScript");
            DescricaoScript = getTokenValue(json, "descricaoScript");
            TipoScript = (TipoScript)System.Enum.Parse(typeof(TipoScript), getTokenValue(json, "tipoScript"));
            TipoObjeto = (TipoObjeto)System.Enum.Parse(typeof(TipoObjeto), getTokenValue(json, "tipoObjeto"));
            Mnemonico = getTokenValue(json, "mnemonico");
            NomeTabelaPai = getTokenValue(json, "nomeTabelaPai");
            TxtScript = getTokenValue(json, "txtScript");

            Validado = false;
            if (!string.IsNullOrEmpty(getTokenValue(json, "validado")))
                Validado = System.Convert.ToBoolean(getTokenValue(json, "validado"));
        }
    }
}
