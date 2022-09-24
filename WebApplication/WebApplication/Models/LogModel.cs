using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebApplication.Models
{
    public partial class LogModel : DbContext
    {
        public LogModel()
            : base("name=System")
        {
        }

        public virtual DbSet<Log> Log { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Log>()
                .Property(e => e.lg01)
                .IsUnicode(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.lg03)
                .IsUnicode(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.lg04)
                .IsUnicode(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.lg05)
                .IsUnicode(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.lg06)
                .IsUnicode(false);
        }
    }
}
