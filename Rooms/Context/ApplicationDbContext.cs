using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rooms.Models;

namespace Rooms.Context
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedRoles(builder);
        }
        private static void SeedRoles(ModelBuilder model)
        {
            model.Entity<IdentityRole>().HasData(
                new IdentityRole() { Name = "User", ConcurrencyStamp = "1", NormalizedName = "User" },
                new IdentityRole() { Name = "Admin", ConcurrencyStamp = "2", NormalizedName = "Admin" }
                );
        }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Bookeds> Orders { get; set; }
        public DbSet<RoomPhotos> Photos { get; set; }
        public DbSet<Contackt> Contackts { get; set; }
    }
}
