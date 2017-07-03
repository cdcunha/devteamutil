namespace DevTeamUtils.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ConexaoInformix")]
    public partial class ConexaoInformix
    {
        [Key]
        public long Id { get; set; }

        public string NomeServidor { get; set; }

        public string Ip { get; set; }

        public long? Porta { get; set; }

        public string Usuario { get; set; }

        public string Senha { get; set; }
    }
}
