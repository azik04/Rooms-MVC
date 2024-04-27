using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rooms.Models;

namespace Rooms.Context
{
    public class ApplicationDbContext : IdentityDbContext<Users>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Comments> Comments { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Bookeds> Orders { get; set; }
        public DbSet<RoomPhotos> Photos { get; set; }
    }
}
