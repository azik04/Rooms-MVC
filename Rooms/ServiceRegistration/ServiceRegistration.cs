using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Rooms.Context;
using Rooms.Models;
using Rooms.Services.Implementations;
using Rooms.Services.Interfaces;

namespace Rooms.ServiceRegistration
{
    public static class ServiceRegistration
    {
        public static void Register(this IServiceCollection Services, IConfiguration config)
        {

            Services.AddScoped<IRoomService, RoomService>();
            Services.AddScoped<IOrderService, OrderService>();
            Services.AddScoped<ICommentService, CommentService>();

            Services.AddIdentity<Users, IdentityRole>()
     .AddEntityFrameworkStores<ApplicationDbContext>()
     .AddDefaultTokenProviders();

            //Services.Configure<IdentityOptions>(options =>
            //{
            //    //lockout settings
            //    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            //    options.Lockout.MaxFailedAccessAttempts = 5;
            //    options.Lockout.AllowedForNewUsers = true;

            //    options.Password.RequireDigit = true;
            //    options.Password.RequiredLength = 8;
            //    options.Password.RequireNonAlphanumeric = true;
            //    options.Password.RequireUppercase = true;
            //    options.Password.RequiredUniqueChars = 1;

            //    //email tesdiqlenmelidir
            //    options.SignIn.RequireConfirmedEmail = false;

            //    options.User.RequireUniqueEmail = false;
            //});

            Services.AddDbContext<ApplicationDbContext>(opt =>
            {
                opt.UseSqlServer(config.GetConnectionString("DefaultConnection"));

            });
        }
    }
}
