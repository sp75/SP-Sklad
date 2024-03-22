namespace Test.DB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model110")
        {
        }

        public virtual DbSet<v_PayDoc> v_PayDoc { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<v_PayDoc>()
                .Property(e => e.Total)
                .HasPrecision(15, 2);

            modelBuilder.Entity<v_PayDoc>()
                .Property(e => e.DocNum)
                .IsUnicode(false);

            modelBuilder.Entity<v_PayDoc>()
                .Property(e => e.Reason)
                .IsUnicode(false);

            modelBuilder.Entity<v_PayDoc>()
                .Property(e => e.Notes)
                .IsUnicode(false);

            modelBuilder.Entity<v_PayDoc>()
                .Property(e => e.OnValue)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_PayDoc>()
                .Property(e => e.Schet)
                .IsUnicode(false);

            modelBuilder.Entity<v_PayDoc>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<v_PayDoc>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<v_PayDoc>()
                .Property(e => e.District)
                .IsUnicode(false);

            modelBuilder.Entity<v_PayDoc>()
                .Property(e => e.Country)
                .IsUnicode(false);

            modelBuilder.Entity<v_PayDoc>()
                .Property(e => e.PostIndex)
                .IsUnicode(false);

            modelBuilder.Entity<v_PayDoc>()
                .Property(e => e.AccNum)
                .IsUnicode(false);

            modelBuilder.Entity<v_PayDoc>()
                .Property(e => e.SummInCurr)
                .HasPrecision(15, 2);

            modelBuilder.Entity<v_PayDoc>()
                .Property(e => e.ActualSummInCurr)
                .HasPrecision(15, 2);

            modelBuilder.Entity<v_PayDoc>()
                .Property(e => e.KaAccNum)
                .IsUnicode(false);

            modelBuilder.Entity<v_PayDoc>()
                .Property(e => e.BankCommission)
                .HasPrecision(15, 2);
        }
    }
}
