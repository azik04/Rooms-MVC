using Rooms.Models.Base;

namespace Rooms.Models
{
    public class Bookeds : BaseModel
    {
        public string? CheckIn { get; set; }
        public string? CheckOut { get; set; }
        public int? UserId { get; set; }
        public Register User { get; set; }
        public int? RoomId { get; set; }
        public Room Room { get; set; }
        public string? Request { get; set; }
    }
}
