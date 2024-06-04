using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Test.DB
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model115")
        {
        }

        public virtual DbSet<v_WaybillMove> v_WaybillMove { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<v_WaybillMove>()
                .Property(e => e.SummAll)
                .HasPrecision(15, 2);

            modelBuilder.Entity<v_WaybillMove>()
                .Property(e => e.SummPay)
                .HasPrecision(15, 2);

            modelBuilder.Entity<v_WaybillMove>()
                .Property(e => e.SummInCurr)
                .HasPrecision(15, 2);

            modelBuilder.Entity<v_WaybillMove>()
                .Property(e => e.DefTotalAmount)
                .HasPrecision(38, 4);

            modelBuilder.Entity<v_WaybillMove>()
                .Property(e => e.ExTotalAmount)
                .HasPrecision(38, 8);
        }
    }
}
