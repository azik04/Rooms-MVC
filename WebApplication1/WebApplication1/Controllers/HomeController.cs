using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Models;
using WebApplication1.Models.Authentication.SignUp;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(Register reg)
        {
            var user = await _userManager.FindByEmailAsync(reg.Email);
            if (user != null)
            {
                return BadRequest(Error);
            }
            IdentityUser newUser = new()
            {
                UserName = reg.UserName,
                SecurityStamp = Guid.NewGuid().ToString(),
                Email = reg.Email
            };
            var result = await _userManager.CreateAsync(newUser);
            await  _userManager.AddToRoleAsync(newUser, "User");
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return BadRequest(Error);
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}