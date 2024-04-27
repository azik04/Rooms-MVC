using System.ComponentModel.DataAnnotations;

namespace Rooms.ViewModels
{
    public class ResetPasswordVM
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
