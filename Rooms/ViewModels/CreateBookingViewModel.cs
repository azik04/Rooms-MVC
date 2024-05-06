using Microsoft.AspNetCore.Mvc;
using Rooms.Models;
using System.ComponentModel.DataAnnotations;

namespace Rooms.ViewModels
{
    public class CreateBookingViewModel
    {
        public string? CheckIn { get; set; }
        public string? CheckOut { get; set; }
        public string? Request { get; set; }
        public string? UserName { get; set; }
        public int? RoomId { get; set; }
        public List<Room>? Rooms { get; set; }

    }
}
