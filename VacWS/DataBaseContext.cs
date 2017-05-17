namespace VacWS
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DataBaseContext : DbContext
    {
        public DataBaseContext()
            : base("name=DataBaseContext")
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Barn> Barn { get; set; }
        public virtual DbSet<Skema> Skema { get; set; }
        public virtual DbSet<VacPlan> VacPlan { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Barn>()
                .Property(e => e.Fornavn)
                .IsFixedLength();

            modelBuilder.Entity<Barn>()
                .Property(e => e.Efternavn)
                .IsFixedLength();

            modelBuilder.Entity<Skema>()
                .Property(e => e.VaccineNavn)
                .IsFixedLength();

            modelBuilder.Entity<Skema>()
                .Property(e => e.Note)
                .IsFixedLength();
        }
    }
}
