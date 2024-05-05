using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rooms.Models;
using Rooms.Services.Implementations;
using Rooms.Services.Interfaces;
using Rooms.ViewModels;
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
            _orderService.Create(order);
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var data = _orderService.GetOrders();
            return View(data);
        }
        public async Task<IActionResult> RemoveOrder(int id)
        {
            _orderService.Remove(id);
            return RedirectToAction("GetAllOrders", "Order");
        }
    }
}
