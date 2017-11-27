using Newtonsoft.Json.Linq;

namespace DevTeamUtils.Api.DTO
{
    public class LoginDTO : Models.Base
    {
        public string Apelido { get; set; }
        public string Senha { get; set; }

        public void DeserializeJson(JObject json)
        {
            //Id = ((JValue)json.SelectToken("bairro")).Value.ToBson();
            Apelido = getTokenValue(json, "apelido");
            Senha = getTokenValue(json, "senha");
            
            //Exemplo para pegar boolean
            /*
            if (!string.IsNullOrEmpty(getTokenValue(json, "ativo")))
                Ativo = Convert.ToBoolean(getTokenValue(json, "ativo"));
            */
        }
    }
}
