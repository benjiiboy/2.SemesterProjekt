namespace VacAppWS
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class VaccAppContext : DbContext
    {
        public VaccAppContext()
            : base("name=VaccAppContext")
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Barn> Barn { get; set; }
        public virtual DbSet<VacPlan> VacPlan { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Barn>()
                .Property(e => e.Fornavn)
                .IsUnicode(false);

            modelBuilder.Entity<Barn>()
                .Property(e => e.Efternavn)
                .IsUnicode(false);

            modelBuilder.Entity<VacPlan>()
                .Property(e => e.Note)
                .IsFixedLength();

            modelBuilder.Entity<VacPlan>()
                .Property(e => e.VaccineNavn)
                .IsFixedLength();
        }
    }
}
