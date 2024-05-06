using System.ComponentModel.DataAnnotations;

namespace Rooms.ViewModels
{
    public class CreateRoomViewModel
    {
        [Required(ErrorMessage = "Please, wright your RoomName")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Please, wright your Room Description")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Please, wright your Room Photo")]
        public List<IFormFile>? RoomPhotos { get; set; }
    }
}
