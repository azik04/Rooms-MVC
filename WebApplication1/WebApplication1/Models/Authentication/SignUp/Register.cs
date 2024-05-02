using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models.Authentication.SignUp
{
    public class Register
    {
        public int Id {get; set;}
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}
