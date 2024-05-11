using Rooms.Models.Base;

namespace Rooms.Models
{
    public class Room: BaseModel
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<RoomPhotos> RoomPhotos { get; set; } 
        public List<Comments>? Comment { get; set; }
    }
}
