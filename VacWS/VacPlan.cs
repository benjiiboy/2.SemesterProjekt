namespace VacWS
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

        public DateTime VaccineTid { get; set; }

        public bool TrueFalse { get; set; }

        public int? Barn_Id { get; set; }

        public int? Vac_Id { get; set; }

        public virtual Barn Barn { get; set; }

        public virtual Skema Skema { get; set; }
    }
}
