namespace Test.DB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model18")
        {
        }

        public virtual DbSet<Materials> Materials { get; set; }
        public virtual DbSet<RemoteCustomerReturned> RemoteCustomerReturned { get; set; }
        public virtual DbSet<WaybillDet> WaybillDet { get; set; }
        public virtual DbSet<WaybillList> WaybillList { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Materials>()
                .Property(e => e.MinReserv)
                .HasPrecision(15, 2);

            modelBuilder.Entity<Materials>()
                .Property(e => e.Weight)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Materials>()
                .Property(e => e.MSize)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Materials>()
                .Property(e => e.NDS)
                .HasPrecision(15, 2);

            modelBuilder.Entity<Materials>()
                .HasMany(e => e.RemoteCustomerReturned)
                .WithRequired(e => e.Materials)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Materials>()
                .HasMany(e => e.WaybillDet)
                .WithRequired(e => e.Materials)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RemoteCustomerReturned>()
                .Property(e => e.Amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<WaybillDet>()
                .Property(e => e.Amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<WaybillDet>()
                .Property(e => e.Price)
                .HasPrecision(15, 4);

            modelBuilder.Entity<WaybillDet>()
                .Property(e => e.Discount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<WaybillDet>()
                .Property(e => e.Nds)
                .HasPrecision(15, 4);

            modelBuilder.Entity<WaybillDet>()
                .Property(e => e.OnValue)
                .HasPrecision(15, 4);

            modelBuilder.Entity<WaybillDet>()
                .Property(e => e.Total)
                .HasPrecision(15, 2);

            modelBuilder.Entity<WaybillDet>()
                .Property(e => e.BasePrice)
                .HasPrecision(15, 4);

            modelBuilder.Entity<WaybillDet>()
                .Property(e => e.AvgInPrice)
                .HasPrecision(15, 4);

            modelBuilder.Entity<WaybillDet>()
                .HasMany(e => e.RemoteCustomerReturned)
                .WithRequired(e => e.WaybillDet)
                .HasForeignKey(e => e.OutPosId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WaybillDet>()
                .HasMany(e => e.RemoteCustomerReturned1)
                .WithOptional(e => e.WaybillDet1)
                .HasForeignKey(e => e.PosId);

            modelBuilder.Entity<WaybillList>()
                .Property(e => e.SummAll)
                .HasPrecision(15, 2);

            modelBuilder.Entity<WaybillList>()
                .Property(e => e.Nds)
                .HasPrecision(15, 4);

            modelBuilder.Entity<WaybillList>()
                .Property(e => e.SummPay)
                .HasPrecision(15, 2);

            modelBuilder.Entity<WaybillList>()
                .Property(e => e.SummInCurr)
                .HasPrecision(15, 2);

            modelBuilder.Entity<WaybillList>()
                .Property(e => e.OnValue)
                .HasPrecision(15, 4);

            modelBuilder.Entity<WaybillList>()
                .HasMany(e => e.WaybillDet)
                .WithRequired(e => e.WaybillList)
                .WillCascadeOnDelete(false);
        }
    }
}
