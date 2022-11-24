namespace Test.DB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model12")
        {
        }

        public virtual DbSet<v_Kagent> v_Kagent { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<v_Kagent>()
                .Property(e => e.FullUrADDR)
                .IsUnicode(false);

            modelBuilder.Entity<v_Kagent>()
                .Property(e => e.FullFactADDR)
                .IsUnicode(false);

            modelBuilder.Entity<v_Kagent>()
                .Property(e => e.StartSaldo)
                .HasPrecision(15, 8);

            modelBuilder.Entity<v_Kagent>()
                .Property(e => e.PriceName)
                .IsUnicode(false);
        }
    }
}
