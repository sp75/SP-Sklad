namespace Test.DB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<KagentList> KagentList { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KagentList>()
                .Property(e => e.FullUrADDR)
                .IsUnicode(false);

            modelBuilder.Entity<KagentList>()
                .Property(e => e.FullFactADDR)
                .IsUnicode(false);

            modelBuilder.Entity<KagentList>()
                .Property(e => e.StartSaldo)
                .HasPrecision(15, 8);

            modelBuilder.Entity<KagentList>()
                .Property(e => e.PriceName)
                .IsUnicode(false);

            modelBuilder.Entity<KagentList>()
                .Property(e => e.Saldo)
                .HasPrecision(38, 2);
        }
    }
}
