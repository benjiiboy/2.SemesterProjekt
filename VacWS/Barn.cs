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
            Vacplan = new HashSet<Vacplan>();
        }

        [Key]
        public int Barn_Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Fornavn { get; set; }

        [Required]
        [StringLength(100)]
        public string Efternavn { get; set; }

        public DateTime Fødselsdato { get; set; }

        public int TelefonNr { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vacplan> Vacplan { get; set; }
    }
}
