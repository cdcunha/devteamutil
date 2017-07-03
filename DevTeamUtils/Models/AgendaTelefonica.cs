namespace DevTeamUtils.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AgendaTelefonica")]
    public partial class AgendaTelefonica
    {
        [Key]
        public long Id { get; set; }

        public string Nome { get; set; }

        public string Telefone { get; set; }

        public string Cargo { get; set; }

        public string Local { get; set; }

        public string Observacao { get; set; }
    }
}
