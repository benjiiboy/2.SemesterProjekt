namespace VaccAppWS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Børn
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Fornavn { get; set; }

        [Required]
        [StringLength(50)]
        public string Efternavn { get; set; }

        public int Fødselsdato { get; set; }

        public int TelefonNr { get; set; }
    }
}
