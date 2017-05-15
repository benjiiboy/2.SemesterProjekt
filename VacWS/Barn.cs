namespace VacWS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Barn")]
    public partial class Barn
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Barn()
        {
            VacPlan = new HashSet<VacPlan>();
        }

        [Key]
        public int Barn_Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Fornavn { get; set; }

        [Required]
        [StringLength(50)]
        public string Efternavn { get; set; }

        [Column(TypeName = "date")]
        public DateTime Fødselsdato { get; set; }

        public int TelefonNr { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VacPlan> VacPlan { get; set; }
    }
}
