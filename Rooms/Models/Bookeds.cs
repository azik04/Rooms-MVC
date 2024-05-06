using Rooms.Models.Base;

namespace Rooms.Models
{
    public class Bookeds : BaseModel
    {
        public string? CheckIn { get; set; }
        public string? CheckOut { get; set; }
        public string? UserName { get; set; }
        public int? RoomId { get; set; }
        public Room Room { get; set; }
        public string? Request { get; set; }
    }
}
