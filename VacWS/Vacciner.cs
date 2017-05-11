namespace VacWS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Vacciner")]
    public partial class Vacciner
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vacciner()
        {
            VacPlan = new HashSet<VacPlan>();
        }

        [Key]
        public int Vac_Id { get; set; }

        [Required]
        [StringLength(1000)]
        public string Note { get; set; }

        [Required]
        [StringLength(100)]
        public string Navn { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VacPlan> VacPlan { get; set; }
    }
}
