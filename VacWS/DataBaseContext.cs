namespace VacWS
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("name=DatabaseContext")
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Barn> Barn { get; set; }
        public virtual DbSet<Vaccine> Vaccine { get; set; }
        public virtual DbSet<Vacplan> Vacplan { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Barn>()
                .Property(e => e.Fornavn)
                .IsFixedLength();

            modelBuilder.Entity<Barn>()
                .Property(e => e.Efternavn)
                .IsFixedLength();

            modelBuilder.Entity<Barn>()
                .HasMany(e => e.Vacplan)
                .WithRequired(e => e.Barn)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vaccine>()
                .Property(e => e.Navn)
                .IsFixedLength();

            modelBuilder.Entity<Vaccine>()
                .Property(e => e.Note)
                .IsFixedLength();

            modelBuilder.Entity<Vaccine>()
                .HasMany(e => e.Vacplan)
                .WithRequired(e => e.Vaccine)
                .WillCascadeOnDelete(false);
        }
    }
}
