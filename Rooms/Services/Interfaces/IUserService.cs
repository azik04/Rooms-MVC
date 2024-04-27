using Rooms.Models;
using Rooms.ViewModels;

namespace Rooms.Services.Interfaces
{
    public interface IUserService
    {
        Task<Users> Register (RegisterViewModel user);
        Task<Users> LogIn(LogInViewModel vm);
        Task<Users> VerifyEmail(string token, string email);
        Task<Users> ResetPassword(string email, string token);
    }
}
