namespace Test.DB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model114")
        {
        }

        public virtual DbSet<v_WayBillOut> v_WayBillOut { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<v_WayBillOut>()
                .Property(e => e.SummAll)
                .HasPrecision(15, 2);

            modelBuilder.Entity<v_WayBillOut>()
                .Property(e => e.Nds)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_WayBillOut>()
                .Property(e => e.SummPay)
                .HasPrecision(15, 2);

            modelBuilder.Entity<v_WayBillOut>()
                .Property(e => e.SummInCurr)
                .HasPrecision(15, 2);

            modelBuilder.Entity<v_WayBillOut>()
                .Property(e => e.CurrRate)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_WayBillOut>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<v_WayBillOut>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<v_WayBillOut>()
                .Property(e => e.District)
                .IsUnicode(false);

            modelBuilder.Entity<v_WayBillOut>()
                .Property(e => e.Country)
                .IsUnicode(false);

            modelBuilder.Entity<v_WayBillOut>()
                .Property(e => e.PostIndex)
                .IsUnicode(false);

            modelBuilder.Entity<v_WayBillOut>()
                .Property(e => e.DefTotalAmount)
                .HasPrecision(38, 4);

            modelBuilder.Entity<v_WayBillOut>()
                .Property(e => e.ExTotalAmount)
                .HasPrecision(38, 8);

            modelBuilder.Entity<v_WayBillOut>()
                .Property(e => e.Balans)
                .HasPrecision(17, 2);

            modelBuilder.Entity<v_WayBillOut>()
                .Property(e => e.TotalAmount)
                .HasPrecision(38, 4);
        }
    }
}
