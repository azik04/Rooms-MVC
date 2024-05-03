using Rooms.Models.Base;

namespace Rooms.Models
{
    public class RoomPhotos : BaseModel
    {
        public string PhotoName { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}
