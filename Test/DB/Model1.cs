namespace Test.DB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model11")
        {
        }

        public virtual DbSet<Tara> Tara { get; set; }
        public virtual DbSet<TaraGroup> TaraGroup { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tara>()
                .Property(e => e.Weight)
                .HasPrecision(15, 4);
        }
    }
}
