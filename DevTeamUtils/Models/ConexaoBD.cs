namespace DevTeamUtils.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("conexaoBD")]
    public partial class ConexaoBD
    {
        [Key]
        public long Id { get; set; }

        public string Tipo { get; set; }

        /// <summary>
        /// NomeServidor para Informix e Service Name para Oracle
        /// </summary>
        public string NomeServidor { get; set; }

        /// <summary>
        /// IP para Informix e Host para Oracle
        /// </summary>
        public string Ip { get; set; }

        public long Porta { get; set; }

        public string Usuario { get; set; }

        public string Senha { get; set; }

        public string Status { get; set; }

        public string DataStatus { get; set; }
    }
}
