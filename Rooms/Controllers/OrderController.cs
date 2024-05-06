using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rooms.Models;
using Rooms.Services.Implementations;
using Rooms.Services.Interfaces;
using Rooms.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Rooms.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IRoomService _service;
        public OrderController(IOrderService orderService, IRoomService service )
        {
            _service = service;
            _orderService = orderService;
        }
        [HttpGet]
        public async Task<IActionResult> Booking()
        {
            var data = _service.GetAll();
            var viewModel = new CreateBookingViewModel
            {
                Rooms = data
            };
            return View(viewModel);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Booking(CreateBookingViewModel order)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            string jwtToken = Request.Cookies["JWT"];

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwtToken);
            var username = token.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name).Value;
            order.UserName = username;
            await _orderService.Create(order, username); 
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllOrders()
        {
            var data = _orderService.GetOrders();
            return View(data);
        }

        //[HttpGet]
        //[Authorize]
        //public async Task<IActionResult> GetByUser()
        //{
        //    string jwtToken = Request.Cookies["JWT"];

        //    var handler = new JwtSecurityTokenHandler();
        //    var token = handler.ReadJwtToken(jwtToken);
        //    var username = token.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
        //    var datas = _orderService.GetByUser(username);

        //    return View(datas);
        //}
        [Authorize]
        public async Task<IActionResult> RemoveOrder(int id)
        {
            _orderService.Remove(id);
            return RedirectToAction("GetAllOrders", "Order");
        }
    }
}
