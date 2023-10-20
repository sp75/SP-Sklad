namespace Test.DB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model13")
        {
        }

        public virtual DbSet<v_WaybillDet> v_WaybillDet { get; set; }
        public virtual DbSet<v_WaybillList> v_WaybillList { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<v_WaybillDet>()
                .Property(e => e.Amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_WaybillDet>()
                .Property(e => e.Price)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_WaybillDet>()
                .Property(e => e.Discount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_WaybillDet>()
                .Property(e => e.Nds)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_WaybillDet>()
                .Property(e => e.OnValue)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_WaybillDet>()
                .Property(e => e.Total)
                .HasPrecision(15, 2);

            modelBuilder.Entity<v_WaybillDet>()
                .Property(e => e.BasePrice)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_WaybillDet>()
                .Property(e => e.AvgInPrice)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_WaybillDet>()
                .Property(e => e.GrpName)
                .IsUnicode(false);

            modelBuilder.Entity<v_WaybillList>()
                .Property(e => e.SummAll)
                .HasPrecision(15, 2);

            modelBuilder.Entity<v_WaybillList>()
                .Property(e => e.Nds)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_WaybillList>()
                .Property(e => e.SummPay)
                .HasPrecision(15, 2);

            modelBuilder.Entity<v_WaybillList>()
                .Property(e => e.SummInCurr)
                .HasPrecision(15, 2);

            modelBuilder.Entity<v_WaybillList>()
                .Property(e => e.OnValue)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_WaybillList>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<v_WaybillList>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<v_WaybillList>()
                .Property(e => e.District)
                .IsUnicode(false);

            modelBuilder.Entity<v_WaybillList>()
                .Property(e => e.Region)
                .IsUnicode(false);

            modelBuilder.Entity<v_WaybillList>()
                .Property(e => e.Country)
                .IsUnicode(false);

            modelBuilder.Entity<v_WaybillList>()
                .Property(e => e.PostIndex)
                .IsUnicode(false);

            modelBuilder.Entity<v_WaybillList>()
                .Property(e => e.CType)
                .IsUnicode(false);

            modelBuilder.Entity<v_WaybillList>()
                .Property(e => e.EntAddress)
                .IsUnicode(false);

            modelBuilder.Entity<v_WaybillList>()
                .Property(e => e.EntCity)
                .IsUnicode(false);

            modelBuilder.Entity<v_WaybillList>()
                .Property(e => e.EntDistrict)
                .IsUnicode(false);

            modelBuilder.Entity<v_WaybillList>()
                .Property(e => e.EntRegion)
                .IsUnicode(false);

            modelBuilder.Entity<v_WaybillList>()
                .Property(e => e.EntPostIndex)
                .IsUnicode(false);

            modelBuilder.Entity<v_WaybillList>()
                .Property(e => e.EntCType)
                .IsUnicode(false);

            modelBuilder.Entity<v_WaybillList>()
                .Property(e => e.AddressSel)
                .IsUnicode(false);

            modelBuilder.Entity<v_WaybillList>()
                .Property(e => e.AddressBuy)
                .IsUnicode(false);

            modelBuilder.Entity<v_WaybillList>()
                .Property(e => e.Balans)
                .HasPrecision(17, 2);
        }
    }
}
