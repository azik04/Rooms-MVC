using Rooms.Models.Base;

namespace Rooms.Models
{
    public class Comments : BaseModel
    {
        public string Body { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }
    }
}
