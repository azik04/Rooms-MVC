using System.ComponentModel.DataAnnotations;

namespace Rooms.ViewModels
{
    public class ResetPasswordVM
    {
        public string Token { get; set; }
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
