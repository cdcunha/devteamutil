using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTeamUtils.Api.DTO
{
    public class StatusDbDTO : Models.Base
    {
        public string Error { get; set; }
        public string Server { get; set; }
        public string Version { get; set; }
        public string Status { get; set; }

        public void DeserializeJson(JObject json)
        {
            //Id = ((JValue)json.SelectToken("bairro")).Value.ToBson();
            Error = getTokenValue(json, "error");
            Server = getTokenValue(json, "server");
            Version = getTokenValue(json, "version");
            Status = getTokenValue(json, "status");

            /*Exemplo de campo boolean
             * if (!string.IsNullOrEmpty(getTokenValue(json, "ativo")))
                Ativo = Convert.ToBoolean(getTokenValue(json, "ativo"));
            */
        }
    }
}
