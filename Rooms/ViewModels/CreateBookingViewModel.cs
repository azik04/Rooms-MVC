using Rooms.Models;

namespace Rooms.ViewModels
{
    public class CreateBookingViewModel
    {
        public string? CheckIn { get; set; }
        public string? CheckOut { get; set; }
        public int? UserId { get; set; }
        public string? Request { get; set; }
        public List<Room> Rooms { get; set; }
        public int RoomId { get; set; }

    }
}
