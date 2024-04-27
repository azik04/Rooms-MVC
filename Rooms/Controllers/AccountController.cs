using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rooms.Models;
using Rooms.Services.Implementations;
using Rooms.Services.Interfaces;
using Rooms.ViewModels;

namespace Rooms.Controllers
{
    public class AccountController : Controller
    {
        private readonly RoleManager<Users> _roleManager;
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        private readonly IMailService _mailService;
        public AccountController(RoleManager<Users> roleManager, UserManager<Users> userManager, SignInManager<Users> signInManager, IMailService mailService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _mailService = mailService;
        }


        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {
            var uploadDirectory = Path.Combine("wwwroot", "img");

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(registerVM.Photo.FileName);
            var saveFilePath = Path.Combine(uploadDirectory, fileName);

            using (var stream = new FileStream(saveFilePath, FileMode.Create))
            {
                registerVM.Photo.CopyToAsync(stream);

            }
            Users appUser = new()
            {
                Email = registerVM.Email,
                Name = registerVM.Name,
                Password = registerVM.Password,
                Photo = fileName,
            };

            var result = await _userManager.CreateAsync(appUser, registerVM.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View(registerVM);
            }
            await _userManager.AddToRoleAsync(appUser, "User");

            string token = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);

            var link = Url.Action(action: "verifyemail", controller: "account", values: new { token, appUser.Email }, protocol: Request.Scheme);

            await _mailService.Send("hacibalaev.azik@mail.ru", appUser.Email, link, "Verify Email");

            TempData["verify"] = "Please verify your email";

            return RedirectToAction("index", "home");
        }
        public async Task<IActionResult> VerifyEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return NotFound();
            }
            await _userManager.ConfirmEmailAsync(user, token);
            await _signInManager.SignInAsync(user, true);
            return RedirectToAction(nameof(Info));
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LogInViewModel loginVm)
        {
            Users appUser = await _userManager.FindByEmailAsync(loginVm.Email);

            if (appUser == null)
            {
                ModelState.AddModelError("", "Username or password is not correct");
                return View(loginVm);
            }
            Microsoft.AspNetCore.Identity.SignInResult result =
                await _signInManager.PasswordSignInAsync(appUser, loginVm.Password, isPersistent: false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", "Your account is blocked for 5 minutes");
                    return View(loginVm);
                }
                ModelState.AddModelError("", "Username or password is not correct");
                return View(loginVm);
            }
            return RedirectToAction("index", "home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        public async Task<IActionResult> Info()
        {

            var result = await _userManager.FindByNameAsync(User.Identity.Name);

            Users appUser = await _userManager.FindByNameAsync(result.UserName);

            return View(appUser);
        }

       

        [HttpGet]
        public async Task<IActionResult> ForgetPassword()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return NotFound();
            }
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var link = Url.Action(action: "resetpassword", controller: "account", values: new { token, email }, protocol: Request.Scheme);

            await _mailService.Send("hacibalaev.azik@mail.ru", user.Email, link, "Reset Password");

            return RedirectToAction("index", "home");
        }


        [HttpGet]
        public async Task<IActionResult> ResetPassword(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return NotFound();
            }
            ResetPasswordVM resetPasswordVM = new ResetPasswordVM()
            {
                Token = token,
                Email = user.Email
            };
            return View(resetPasswordVM);
        }


        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM resetPassword)
        {
            var user = await _userManager.FindByEmailAsync(resetPassword.Email);

            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View(result);
            }
            return RedirectToAction("login", "account");
        }
    }
}
