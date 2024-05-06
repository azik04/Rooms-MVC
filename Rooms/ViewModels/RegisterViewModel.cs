using System.ComponentModel.DataAnnotations;

namespace Rooms.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please, wright your UserName")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Please, wright your Email")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Please, wright your Passeord")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
