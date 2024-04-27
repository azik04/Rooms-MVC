using Microsoft.AspNetCore.Mvc;
using Rooms.Models;
using Rooms.Services.Interfaces;
using Rooms.ViewModels;

namespace Rooms.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomService _service;
        private readonly ICommentService _commentService;
        public RoomController(IRoomService service, ICommentService commentService)
        {
            _service = service;
            _commentService = commentService;
        }

        public async Task<IActionResult> CreateRoom()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRoom(CreateRoomViewModel room)
        {
            await _service.Create(room);
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> UpdRoom(int id)
        {
            var data = await _service.Get(id);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> UpdRoom(Room room)
        {
            await _service.Update(room);
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> DeleteRoom(int id)
        {
            await _service.Delete(id);
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _service.Get(id);
            var vm = new CreateCommentViewModel()
            {
                Name = data.Name,
                Description = data.Description,
                RoomPhoto = data.RoomPhotos,
                Comment = data.Comment,
                Id = data.Id
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> GetById(Comments com)
        {
            _commentService.CreateComment(com);
            return RedirectToAction("Index", "Home");
        }
    }
}
