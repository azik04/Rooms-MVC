using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using Org.BouncyCastle.Ocsp;
using Rooms.Models;
using Rooms.Services.Implementations;
using Rooms.Services.Interfaces;
using Rooms.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Rooms.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMailService _mailService;
        public AccountController(SignInManager<IdentityUser> signInManager, IMailService mailService, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _mailService = mailService;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(Register reg)
        {
            var user = await _userManager.FindByEmailAsync(reg.Email);
            if (user != null)
            {
                return BadRequest();
            }
            IdentityUser newUser = new()
            {
                UserName = reg.UserName,
                SecurityStamp = Guid.NewGuid().ToString(),
                Email = reg.Email
            };
            var result = await _userManager.CreateAsync(newUser);
            //await _userManager.AddToRoleAsync(newUser, "User");
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

            var link = Url.Action(action: "verifyemail", controller: "account", values: new { token, newUser.Email }, protocol: Request.Scheme);

            await _mailService.Send("hacibalaev.azik@mail.ru", newUser.Email, link, "Verify Email");

           TempData["verify"] = "Please verify your email";
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return BadRequest();
            }
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
            return RedirectToAction("Home", "Index");
        }

        //[HttpGet]
        //public async Task<IActionResult> Login()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Login(LogInViewModel loginVm)
        //{
        //    Users appUser = await _userManager.FindByEmailAsync(loginVm.Email);

        //    if (appUser == null)
        //    {
        //        ModelState.AddModelError("", "Username or password is not correct");
        //        return View(loginVm);
        //    }
        //    Microsoft.AspNetCore.Identity.SignInResult result =
        //        await _signInManager.PasswordSignInAsync(appUser, loginVm.Password, isPersistent: false, lockoutOnFailure: false);

        //    if (!result.Succeeded)
        //    {
        //        if (result.IsLockedOut)
        //        {
        //            ModelState.AddModelError("", "Your account is blocked for 5 minutes");
        //            return View(loginVm);
        //        }
        //        ModelState.AddModelError("", "Username or password is not correct");
        //        return View(loginVm);
        //    }
        //    return RedirectToAction("index", "home");
        //}

        //public async Task<IActionResult> Logout()
        //{
        //    await _signInManager.SignOutAsync();
        //    return RedirectToAction("index", "home");
        //}

        //public async Task<IActionResult> Info()
        //{

        //    var result = await _userManager.FindByNameAsync(User.Identity.Name);

        //    Users appUser = await _userManager.FindByNameAsync(result.UserName);

        //    return View(appUser);
        //}



        //[HttpGet]
        //public async Task<IActionResult> ForgetPassword()
        //{
        //    return View();
        //}


        //[HttpPost]
        //public async Task<IActionResult> ForgetPassword(string email)
        //{
        //    var user = await _userManager.FindByEmailAsync(email);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    string token = await _userManager.GeneratePasswordResetTokenAsync(user);

        //    var link = Url.Action(action: "resetpassword", controller: "account", values: new { token, email }, protocol: Request.Scheme);

        //    await _mailService.Send("hacibalaev.azik@mail.ru", user.Email, link, "Reset Password");

        //    return RedirectToAction("index", "home");
        //}


        //[HttpGet]
        //public async Task<IActionResult> ResetPassword(string email, string token)
        //{
        //    var user = await _userManager.FindByEmailAsync(email);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    ResetPasswordVM resetPasswordVM = new ResetPasswordVM()
        //    {
        //        Token = token,
        //        Email = user.Email
        //    };
        //    return View(resetPasswordVM);
        //}


        //[HttpPost]
        //public async Task<IActionResult> ResetPassword(ResetPasswordVM resetPassword)
        //{
        //    var user = await _userManager.FindByEmailAsync(resetPassword.Email);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    var result = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);

        //    if (!result.Succeeded)
        //    {
        //        foreach (var item in result.Errors)
        //        {
        //            ModelState.AddModelError("", item.Description);
        //        }
        //        return View(result);
        //    }
        //    return RedirectToAction("login", "account");
        //}
    }
}
