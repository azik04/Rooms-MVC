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
        public HomeController(IRoomService service)
        {
            _service = service;
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
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Service()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Room(int pageNumber , int pageSize = 5)
        {
            pageNumber++;
            var data = _service.GetByPage(pageNumber, pageSize);
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