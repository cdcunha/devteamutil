using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace DevTeamUtils.Api.Models
{
    [DataContract]
    public class PassoGerado : BaseModel
    {
        [DataMember]
        public System.Guid PassoId { get; set; }

        [DataMember]
        public string bancoDados { get; set; }

        [DataMember]
        public string TipoInstrucaoSql { get; set; }
        
        [DataMember]
        public string NomeArquivo { get; set; }

        [DataMember]
        public string Passo { get; set; }
    }
}
