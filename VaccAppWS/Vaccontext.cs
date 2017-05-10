namespace VaccAppWS
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Vaccontext : DbContext
    {
        public Vaccontext()
            : base("name=Vaccontext")
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Barn> Barn { get; set; }
        public virtual DbSet<Vacciner> Vacciner { get; set; }
        public virtual DbSet<VacPlan> VacPlan { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Barn>()
                .Property(e => e.Fornavn)
                .IsUnicode(false);

            modelBuilder.Entity<Barn>()
                .Property(e => e.Efternavn)
                .IsUnicode(false);

            modelBuilder.Entity<Barn>()
                .HasMany(e => e.VacPlan)
                .WithRequired(e => e.Barn)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vacciner>()
                .Property(e => e.Note)
                .IsFixedLength();

            modelBuilder.Entity<Vacciner>()
                .Property(e => e.Navn)
                .IsFixedLength();

            modelBuilder.Entity<Vacciner>()
                .HasMany(e => e.VacPlan)
                .WithRequired(e => e.Vacciner)
                .WillCascadeOnDelete(false);
        }
    }
}
