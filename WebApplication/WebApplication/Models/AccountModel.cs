using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebApplication.Models
{
    public partial class AccountModel : DbContext
    {
        public AccountModel()
            : base("name=System")
        {
        }

        public virtual DbSet<Account> Account { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(e => e.ac01)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.ac02)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.ac04)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.ac06)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.ac07)
                .IsUnicode(false);
        }
    }
}
