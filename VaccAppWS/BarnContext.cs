namespace VaccAppWS
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BarnContext : DbContext
    {
        public BarnContext()
            : base("name=BarnContext")
        {
            base.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Børn> Børn { get; set; }
        public virtual DbSet<VacSkema> VacSkema { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Børn>()
                .Property(e => e.Fornavn)
                .IsUnicode(false);

            modelBuilder.Entity<Børn>()
                .Property(e => e.Efternavn)
                .IsUnicode(false);

            modelBuilder.Entity<VacSkema>()
                .Property(e => e.Navn)
                .IsUnicode(false);
        }
    }
}
