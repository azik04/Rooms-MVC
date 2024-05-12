using Microsoft.AspNetCore.Mvc;
using Rooms.Context;
using Rooms.Models;
using Rooms.Services.Interfaces;
using Rooms.ViewModels;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;

namespace Rooms.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRoomService _service;
        private readonly IContactService _contactService;
        public HomeController(IRoomService service, IContactService contactService)
        {
            _service = service;
            _contactService = contactService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var data = _service.GetAll();
            var room = data.Take(3).ToList();
                return View(room);
            
        }
        public IActionResult About()
        {
            return View();
        }
        public async Task<IActionResult> Contact()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Contact(Contackt contact)
        {
            _contactService.Create(contact);
            return RedirectToAction("Index");
        }
        public IActionResult Service()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Room(int page)
        {
            var data = _service.GetByPage(page);
            return View(data);
        }
        public IActionResult OurTeam()
        {
            return View();
        }
        public IActionResult Testimonial()
        {
            return View();
        }
    }
}