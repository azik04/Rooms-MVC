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
        public string Body { get; set; }
        public string UserName { get; set; }
        public int RoomId { get; set; }

    }
}
