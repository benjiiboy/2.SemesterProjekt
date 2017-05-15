namespace VacAppWS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VacPlan")]
    public partial class VacPlan
    {
        [Key]
        public int Plan_Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime Tid { get; set; }

        public bool TrueFalse { get; set; }

        [Required]
        [StringLength(1000)]
        public string Note { get; set; }

        [Required]
        [StringLength(100)]
        public string VaccineNavn { get; set; }

        public int Barn_Id { get; set; }
    }
}
