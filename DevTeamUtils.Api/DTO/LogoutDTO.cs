using Newtonsoft.Json.Linq;

namespace DevTeamUtils.Api.DTO
{
    public class LogoutDTO : Models.Base
    {
        public string Apelido { get; set; }
        public string ConnectionId { get; set; }

        public void DeserializeJson(JObject json)
        {
            //Id = ((JValue)json.SelectToken("bairro")).Value.ToBson();
            Apelido = getTokenValue(json, "apelido");
            ConnectionId = getTokenValue(json, "connectionId");
            
            //Exemplo para pegar boolean
            /*
            if (!string.IsNullOrEmpty(getTokenValue(json, "ativo")))
                Ativo = Convert.ToBoolean(getTokenValue(json, "ativo"));
            */
        }
    }
}
