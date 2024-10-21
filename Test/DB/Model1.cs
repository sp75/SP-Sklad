using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Test.DB
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model117")
        {
        }

        public virtual DbSet<v_wrd_CustomerOrders> v_wrd_CustomerOrders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<v_wrd_CustomerOrders>()
                .Property(e => e.Amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_wrd_CustomerOrders>()
                .Property(e => e.Price)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_wrd_CustomerOrders>()
                .Property(e => e.Total)
                .HasPrecision(15, 2);

            modelBuilder.Entity<v_wrd_CustomerOrders>()
                .Property(e => e.BasePrice)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_wrd_CustomerOrders>()
                .Property(e => e.PrevAmount)
                .HasPrecision(15, 4);
        }
    }
}
