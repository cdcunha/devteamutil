using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTeamUtils.Api.Models
{
    public class Base
    {
        protected string getTokenValue(JObject json, string key)
        {   
            return json.Property(key) != null ? json.Property(key).Value.ToString() : "";
        }
    }
}
