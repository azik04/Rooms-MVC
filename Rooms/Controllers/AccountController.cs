using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using Org.BouncyCastle.Ocsp;
using Rooms.Models;
using Rooms.Services.Implementations;
using Rooms.Services.Interfaces;
using Rooms.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Rooms.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMailService _mailService;
        private readonly IConfiguration _configuration;
        public AccountController(SignInManager<IdentityUser> signInManager, IConfiguration configuration, IMailService mailService, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _mailService = mailService;
            _signInManager = signInManager;
            _configuration = configuration;
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
            var result = await _userManager.CreateAsync(newUser, reg.Password);
            //await _userManager.AddToRoleAsync(newUser, "User");
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

            var link = Url.Action(action: "verifyemail", controller: "account", values: new { token, newUser.Email }, protocol: Request.Scheme);

            await _mailService.Send("hacibalaev.azik@mail.ru", newUser.Email, link, "Verify Email");

           TempData["verify"] = "Please verify your email";
            if (result.Succeeded)
            {
                return RedirectToAction("Confirm", "Account");
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
            return RedirectToAction("LogIn", "Account");
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LogInViewModel loginVm)
        {
            var appUser = await _userManager.FindByEmailAsync(loginVm.Email);
            //if(appUser.EmailConfirmed == false)
            //{
            //    return BadRequest("User Shoud config an email");
            //}
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
            //else
            //{
            //    var authClaim = new List<Claim>
            //    {
            //        new Claim(ClaimTypes.Name , appUser.UserName),
            //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            //    };
            //}
            return RedirectToAction("Index", "Home"); 
        }

        //private JwtSecurityToken GetToken(List<Claim> authClaims)
        //{
        //    var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:secret"]));
        //    var token = new JwtSecurityToken(
        //        issuer: _configuration["JWT:ValidIssuer"],
        //        audience: _configuration["JWT:ValidAudience"],
        //        expires: DateTime.Now.AddHours(3),
        //        claims: authClaims,
        //        signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256)
        //        );
        //    return token;
        //}

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
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

            var link = Url.Action(action: "ResetPassword", controller: "account", values: new { token, email }, protocol: Request.Scheme);

            await _mailService.Send("hacibalaev.azik@mail.ru", user.Email, link, "Reset Password");

            return RedirectToAction("Confirm", "Account");
        }

        public async Task<IActionResult> Confirm()
        {
            return View();
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
                Email = email
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
            return RedirectToAction("login", "account");
        }
    }
}
