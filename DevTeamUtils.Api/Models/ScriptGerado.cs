using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace DevTeamUtils.Api.Models
{
    [DataContract]
    public class ScriptGerado : BaseModel
    {
        [DataMember]
        public System.Guid ScriptId { get; set; }

        [DataMember]
        public string bancoDados { get; set; }

        [DataMember]
        public string TipoInstrucaoSql { get; set; }

        [DataMember]
        public string Script { get; set; }
    }
}
