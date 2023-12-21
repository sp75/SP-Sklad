namespace Test.DB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model14")
        {
        }

        public virtual DbSet<RemoteCustomerOrders> RemoteCustomerOrders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RemoteCustomerOrders>()
                .Property(e => e.Amount)
                .HasPrecision(15, 4);
        }
    }
}
