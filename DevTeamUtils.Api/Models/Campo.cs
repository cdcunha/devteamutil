using DevTeamUtils.Api.Enums;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace DevTeamUtils.Api.Models
{
    [DataContract]
    public class Campo : Base
    {
        [DataMember]
        public string Nome { get; set; }

        [DataMember]
        public string Descricao { get; set; }

        [DataMember]
        public EnumAtributoCampo Atributo { get; set; }

        [DataMember]
        public EnumTipoCampo Tipo { get; set; } 

        [DataMember]
        public int Tamanho { get; set; }

        [DataMember]
        public string Valor { get; set; }

        [DataMember]
        public bool NotNull { get; set; }

        [DataMember]
        public string MnemonicoRefFk { get; set; }

        [DataMember]
        public string FieldRefFk { get; set; }

        public void DeserializeJson(JObject json)
        {
            //Id = ((JValue)json.SelectToken("bairro")).Value.ToBson();
            Nome = getTokenValue(json, "nome");
            Descricao = getTokenValue(json, "descricao");
            Tipo = (EnumTipoCampo)System.Enum.Parse(typeof(EnumTipoCampo), getTokenValue(json, "tipo"));
            Tamanho = int.Parse(getTokenValue(json, "tamanho"));
            Valor = getTokenValue(json, "valor");
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
