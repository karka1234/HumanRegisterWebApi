using HumanRegisterWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HumanRegisterWebApi.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<UserAddress> UserAddreses { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.UserName).IsUnique();//del jwt logikos. kita karta su id reiks daryt

            modelBuilder.Entity<User>()
                .HasOne(u => u.UserInfo)
                .WithOne(ui => ui.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserInfo>()
                .HasOne(ui => ui.UserAddress)
                .WithOne(ua => ua.UserInfo)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserInfo>()
                .HasOne(ui => ui.ProfileImage)
                .WithOne(pi => pi.UserInfo)
                .OnDelete(DeleteBehavior.Cascade);


        }

    }
}