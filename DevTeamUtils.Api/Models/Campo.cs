using DevTeamUtils.Api.Enums;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace DevTeamUtils.Api.Models
{
    [DataContract]
    public class Campo : BaseModel
    {
        [DataMember]
        public System.Guid ScriptId { get; set; }

        [DataMember]
        public string NomeCampo { get; set; }

        [DataMember]
        public string DescricaoCampo { get; set; }

        [DataMember]
        public EnumAtributoCampo Atributo { get; set; }

        [DataMember]
        public EnumTipoCampo TipoCampo { get; set; } 

        [DataMember]
        public int TamanhoCampo { get; set; }

        [DataMember]
        public string ValorCampo { get; set; }

        [DataMember]
        public bool NotNull { get; set; }

        [DataMember]
        public string MnemonicoRefFk { get; set; }

        [DataMember]
        public string FieldRefFk { get; set; }

        public void DeserializeJson(JObject json)
        {
            //Id = ((JValue)json.SelectToken("bairro")).Value.ToBson();
            NomeCampo = getTokenValue(json, "nomeCampo");
            DescricaoCampo = getTokenValue(json, "descricaoCampo");
            TipoCampo = (EnumTipoCampo)System.Enum.Parse(typeof(EnumTipoCampo), getTokenValue(json, "tipoCampo"));
            TamanhoCampo = int.Parse(getTokenValue(json, "tamanhoCampo"));
            ValorCampo = getTokenValue(json, "valorCampo");
            if (!string.IsNullOrEmpty(getTokenValue(json, "notNull")))
            {
                NotNull = System.Convert.ToBoolean(getTokenValue(json, "notNull"));
            }            
            Atributo = (EnumAtributoCampo)System.Enum.Parse(typeof(EnumAtributoCampo), getTokenValue(json, "atributo"));
            MnemonicoRefFk = getTokenValue(json, "mnemonicoRefFk");
            FieldRefFk = getTokenValue(json, "fieldRefFk");
        }
    }
}
