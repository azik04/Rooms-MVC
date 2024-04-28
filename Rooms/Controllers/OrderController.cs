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
        public async Task<IActionResult> Booking(CreateBookingViewModel order)
        {
            _orderService.Create(order);
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrders(int pg = 5)
        {
            var data = _orderService.GetOrders();

            const int pageSize = 1;
            if (pg < 1)
                pg = 1;
            int recsCount = data.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var datas = data.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;

            return View(datas);
        }
        public async Task<IActionResult> RemoveOrder(int id)
        {
            _orderService.Remove(id);
            return RedirectToAction("GetAllOrders", "Order");
        }
    }
}
