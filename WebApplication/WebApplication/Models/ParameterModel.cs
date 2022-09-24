using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebApplication.Models
{
    public partial class ParameterModel : DbContext
    {
        public ParameterModel()
            : base("name=System")
        {
        }

        public virtual DbSet<Parameter> Parameter { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Parameter>()
                .Property(e => e.prm01)
                .IsUnicode(false);

            modelBuilder.Entity<Parameter>()
                .Property(e => e.prm02)
                .IsUnicode(false);

            modelBuilder.Entity<Parameter>()
                .Property(e => e.prm04)
                .IsUnicode(false);

            modelBuilder.Entity<Parameter>()
                .Property(e => e.prm06)
                .IsUnicode(false);

            modelBuilder.Entity<Parameter>()
                .Property(e => e.prm07)
                .IsUnicode(false);
        }
    }
}
