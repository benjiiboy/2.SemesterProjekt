namespace VacWS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Vaccine")]
    public partial class Vaccine
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vaccine()
        {
            Vacplan = new HashSet<Vacplan>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Vac_Id { get; set; }

        public int Tid { get; set; }

        [Required]
        [StringLength(50)]
        public string Navn { get; set; }

        [Required]
        [StringLength(1000)]
        public string Note { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vacplan> Vacplan { get; set; }
    }
}
