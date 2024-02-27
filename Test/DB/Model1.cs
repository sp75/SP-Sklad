namespace Test.DB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model15")
        {
        }

        public virtual DbSet<v_WayBillBase> v_WayBillBase { get; set; }
        public virtual DbSet<v_WayBillReturnСustomerDet> v_WayBillReturnСustomerDet { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<v_WayBillBase>()
                .Property(e => e.SummAll)
                .HasPrecision(15, 2);

            modelBuilder.Entity<v_WayBillBase>()
                .Property(e => e.Nds)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_WayBillBase>()
                .Property(e => e.SummPay)
                .HasPrecision(15, 2);

            modelBuilder.Entity<v_WayBillBase>()
                .Property(e => e.SummInCurr)
                .HasPrecision(15, 2);

            modelBuilder.Entity<v_WayBillBase>()
                .Property(e => e.Balans)
                .HasPrecision(17, 2);

            modelBuilder.Entity<v_WayBillBase>()
                .Property(e => e.CurrRate)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_WayBillBase>()
                .Property(e => e.DefTotalAmount)
                .HasPrecision(38, 4);

            modelBuilder.Entity<v_WayBillBase>()
                .Property(e => e.ExTotalAmount)
                .HasPrecision(38, 8);

            modelBuilder.Entity<v_WayBillReturnСustomerDet>()
                .Property(e => e.Amount)
                .HasPrecision(17, 4);

            modelBuilder.Entity<v_WayBillReturnСustomerDet>()
                .Property(e => e.Price)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_WayBillReturnСustomerDet>()
                .Property(e => e.Discount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_WayBillReturnСustomerDet>()
                .Property(e => e.Nds)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_WayBillReturnСustomerDet>()
                .Property(e => e.OnValue)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_WayBillReturnСustomerDet>()
                .Property(e => e.Norm)
                .HasPrecision(15, 8);

            modelBuilder.Entity<v_WayBillReturnСustomerDet>()
                .Property(e => e.Country)
                .IsUnicode(false);

            modelBuilder.Entity<v_WayBillReturnСustomerDet>()
                .Property(e => e.FullPrice)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_WayBillReturnСustomerDet>()
                .Property(e => e.Total)
                .HasPrecision(15, 2);

            modelBuilder.Entity<v_WayBillReturnСustomerDet>()
                .Property(e => e.BasePrice)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_WayBillReturnСustomerDet>()
                .Property(e => e.SumNds)
                .HasPrecision(35, 10);

            modelBuilder.Entity<v_WayBillReturnСustomerDet>()
                .Property(e => e.GrpName)
                .IsUnicode(false);

            modelBuilder.Entity<v_WayBillReturnСustomerDet>()
                .Property(e => e.DiscountPrice)
                .HasPrecision(38, 6);

            modelBuilder.Entity<v_WayBillReturnСustomerDet>()
                .Property(e => e.TotalInCurrency)
                .HasPrecision(31, 6);
        }
    }
}
