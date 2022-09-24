using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebApplication.Models
{
    public partial class GroupModel : DbContext
    {
        public GroupModel()
            : base("name=System")
        {
        }

        public virtual DbSet<Group> Group { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>()
                .Property(e => e.gp02)
                .IsUnicode(false);

            modelBuilder.Entity<Group>()
                .Property(e => e.gp04)
                .IsUnicode(false);

            modelBuilder.Entity<Group>()
                .Property(e => e.gp06)
                .IsUnicode(false);
        }
    }
}
