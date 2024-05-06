using Rooms.Models;
using System.ComponentModel.DataAnnotations;

namespace Rooms.ViewModels
{
    public class CreateCommentViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<RoomPhotos>? RoomPhoto { get; set; }
        public List<Comments>? Comment { get; set; }
        [Required]
        public string Body { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }

    }
}
