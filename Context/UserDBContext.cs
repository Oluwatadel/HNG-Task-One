using Microsoft.EntityFrameworkCore;
using User_Registartion.Entity;

namespace User_Registartion.Context
{
    public class UserDBContext : DbContext
    {
        public UserDBContext(DbContextOptions<UserDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasMany(a => a.Organisations).WithMany(a => a.Users);
            modelBuilder.Entity<User>().HasKey(a => a.UserId);
            modelBuilder.Entity<Organisation>().HasKey(a => a.OrgId);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Organisation> Organisations { get; set; }

    }
}
