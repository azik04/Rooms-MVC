namespace Rooms.ViewModels
{
    public class CreateRoomViewModel
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<IFormFile>? RoomPhotos { get; set; }
    }
}
