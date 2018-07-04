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
        public AtributoCampo Atributo { get; set; }

        [DataMember]
        public TipoCampo TipoCampo { get; set; } 

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

        public Campo() : base(){ }

        public void DeserializeJson(JObject json)
        {
            //Id = ((JValue)json.SelectToken("bairro")).Value.ToBson();
            ScriptId = json.Property("scriptId").Value.ToObject<System.Guid>();
            NomeCampo = getTokenValue(json, "nomeCampo");
            DescricaoCampo = getTokenValue(json, "descricaoCampo");
            Atributo = (AtributoCampo)System.Enum.Parse(typeof(AtributoCampo), getTokenValue(json, "atributo"));
            TipoCampo = (TipoCampo)System.Enum.Parse(typeof(TipoCampo), getTokenValue(json, "tipoCampo"));
            TamanhoCampo = int.Parse(getTokenValue(json, "tamanhoCampo"));
            ValorCampo = getTokenValue(json, "valorCampo");
            NotNull = false;
            if (!string.IsNullOrEmpty(getTokenValue(json, "notNull")))
            {
                NotNull = System.Convert.ToBoolean(getTokenValue(json, "notNull"));
            }
            MnemonicoRefFk = getTokenValue(json, "mnemonicoRefFk");
            FieldRefFk = getTokenValue(json, "fieldRefFk");
        }
    }
}
