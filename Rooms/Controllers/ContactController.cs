using Microsoft.AspNetCore.Mvc;
using Rooms.Services.Interfaces;

namespace Rooms.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = _contactService.GetOrders();
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            var data = _contactService.Remove(id);
            return RedirectToAction("Index", "Home");
        }
    }
}
