namespace DevTeamUtils.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AcionamentoAgfa")]
    public partial class AcionamentoAgfa
    {
        public long Id { get; set; }

        [StringLength(2147483647)]
        public string NomeContato { get; set; }

        [StringLength(2147483647)]
        public string TipoContato { get; set; }

        [StringLength(2147483647)]
        public string DataInicio { get; set; }

        [StringLength(2147483647)]
        public string DataFim { get; set; }

        [StringLength(2147483647)]
        public string CodigoTarefa { get; set; }

        [StringLength(2147483647)]
        public string Motivo { get; set; }

        [StringLength(2147483647)]
        public string Situacao { get; set; }
    }
}
