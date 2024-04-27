using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Rooms.Models;
using Rooms.Services.Interfaces;
using Rooms.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using MimeKit;
using System.Web.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using System;

namespace Rooms.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<Users> _userManager;
        private readonly RoleManager<Users> _roleManager;
        private readonly SignInManager<Users> _signInManager;
        public UserService(
    UserManager<Users> userManager,
    RoleManager<Users> roleManager,
    SignInManager<Users> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }


        public async Task<Users> LogIn(LogInViewModel vm)
        {
            var user = await _userManager.FindByEmailAsync(vm.Email);
            if (user == null)
            {
                return null; 
            }

            await _signInManager.PasswordSignInAsync(user, vm.Password, isPersistent: false, lockoutOnFailure: false);
            return user;
        }

        public async Task<Users> Register(RegisterViewModel user)
        {
            var uploadDirectory = Path.Combine("wwwroot", "img");

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(user.Photo.FileName);
            var saveFilePath = Path.Combine(uploadDirectory, fileName);

            using (var stream = new FileStream(saveFilePath, FileMode.Create))
            {
                user.Photo.CopyToAsync(stream);

            }
            Users data = new Users()
            {
                Email = user.Email,
                Name = user.Name,
                Password = user.Password,
                Photo = fileName,
            };
            await _userManager.CreateAsync(data);
            await _userManager.AddToRoleAsync(data, "User");
            return data;
        }

        public Task<Users> ResetPassword(string email, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<Users> VerifyEmail(string token, string email)
        {
            var user = _userManager.FindByEmailAsync(email);
            await _userManager.ConfirmEmailAsync(await user, token);
            await _signInManager.SignInAsync(await user, true);
            return await user;
        }
    }
}
