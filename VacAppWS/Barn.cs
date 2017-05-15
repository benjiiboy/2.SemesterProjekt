namespace VacAppWS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Barn")]
    public partial class Barn
    {
        [Key]
        public int Barn_Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Fornavn { get; set; }

        [Required]
        [StringLength(50)]
        public string Efternavn { get; set; }

        public DateTime Fødselsdato { get; set; }

        public int TelefonNr { get; set; }
    }
}
