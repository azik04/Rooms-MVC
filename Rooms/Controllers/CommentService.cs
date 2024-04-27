using Microsoft.AspNetCore.Mvc;
using Rooms.Models;
using Rooms.Services.Interfaces;

namespace Rooms.Controllers
{
    public class CommentService : Controller
    {
        private readonly ICommentService _service;
        public CommentService(ICommentService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetByRoom(int id) 
        {
            var data =  _service.GetByRoom(id);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> CreateComment(Comments com)
        {
            _service.CreateComment(com);
            return RedirectToAction("Index", "Home");
        }
    }
}
