using Microsoft.AspNetCore.Identity;

namespace Rooms.Models
{
    public class Users : IdentityUser
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Photo { get; set; }
    }
}
