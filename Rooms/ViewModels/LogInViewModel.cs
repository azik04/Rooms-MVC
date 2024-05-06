using System.ComponentModel.DataAnnotations;

namespace Rooms.ViewModels
{
    public class LogInViewModel
    {
        [Required(ErrorMessage = "Please, wright your Name")]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
