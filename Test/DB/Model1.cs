namespace Test.DB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model19")
        {
        }

        public virtual DbSet<v_WayBillCustomerOrder> v_WayBillCustomerOrder { get; set; }
        public virtual DbSet<v_WayBillCustomerOrderDet> v_WayBillCustomerOrderDet { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<v_WayBillCustomerOrder>()
                .Property(e => e.SummAll)
                .HasPrecision(15, 2);

            modelBuilder.Entity<v_WayBillCustomerOrder>()
                .Property(e => e.Nds)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_WayBillCustomerOrder>()
                .Property(e => e.SummPay)
                .HasPrecision(15, 2);

            modelBuilder.Entity<v_WayBillCustomerOrder>()
                .Property(e => e.SummInCurr)
                .HasPrecision(15, 2);

            modelBuilder.Entity<v_WayBillCustomerOrder>()
                .Property(e => e.CurrRate)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_WayBillCustomerOrder>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<v_WayBillCustomerOrder>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<v_WayBillCustomerOrder>()
                .Property(e => e.District)
                .IsUnicode(false);

            modelBuilder.Entity<v_WayBillCustomerOrder>()
                .Property(e => e.Country)
                .IsUnicode(false);

            modelBuilder.Entity<v_WayBillCustomerOrder>()
                .Property(e => e.PostIndex)
                .IsUnicode(false);

            modelBuilder.Entity<v_WayBillCustomerOrder>()
                .Property(e => e.Balans)
                .HasPrecision(17, 2);

            modelBuilder.Entity<v_WayBillCustomerOrder>()
                .Property(e => e.TotalAmount)
                .HasPrecision(38, 4);

            modelBuilder.Entity<v_WayBillCustomerOrderDet>()
                .Property(e => e.Amount)
                .HasPrecision(17, 4);

            modelBuilder.Entity<v_WayBillCustomerOrderDet>()
                .Property(e => e.Price)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_WayBillCustomerOrderDet>()
                .Property(e => e.Discount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_WayBillCustomerOrderDet>()
                .Property(e => e.Nds)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_WayBillCustomerOrderDet>()
                .Property(e => e.OnValue)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_WayBillCustomerOrderDet>()
                .Property(e => e.Norm)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_WayBillCustomerOrderDet>()
                .Property(e => e.Country)
                .IsUnicode(false);

            modelBuilder.Entity<v_WayBillCustomerOrderDet>()
                .Property(e => e.CardNum)
                .IsUnicode(false);

            modelBuilder.Entity<v_WayBillCustomerOrderDet>()
                .Property(e => e.BasePrice)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_WayBillCustomerOrderDet>()
                .Property(e => e.Total)
                .HasPrecision(15, 2);

            modelBuilder.Entity<v_WayBillCustomerOrderDet>()
                .Property(e => e.SumNds)
                .HasPrecision(35, 10);

            modelBuilder.Entity<v_WayBillCustomerOrderDet>()
                .Property(e => e.DiscountPrice)
                .HasPrecision(38, 6);

            modelBuilder.Entity<v_WayBillCustomerOrderDet>()
                .Property(e => e.AvgInPrice)
                .HasPrecision(38, 6);

            modelBuilder.Entity<v_WayBillCustomerOrderDet>()
                .Property(e => e.DiscountTotal)
                .HasPrecision(32, 8);
        }
    }
}
