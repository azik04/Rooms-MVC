using Microsoft.AspNetCore.Mvc;
using Rooms.Services.Interfaces;

namespace Rooms.Controllers
{
    public class AccountService : Controller
    {
        private readonly IUserService _userService;
        public AccountService(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
