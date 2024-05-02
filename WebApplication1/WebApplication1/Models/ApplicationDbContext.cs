using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.Authentication.SignUp;

namespace WebApplication1.Models
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
                new IdentityRole() {Id = "1", Name = "User", ConcurrencyStamp = "1", NormalizedName = "User" },
                new IdentityRole() {Id = "2", Name = "Admin", ConcurrencyStamp = "2", NormalizedName = "Admin" }
                );
        }
        public DbSet<Register> Register { get; set; }
    }
}
