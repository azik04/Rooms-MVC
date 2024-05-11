using Rooms.Models.Base;

namespace Rooms.Models
{
    public class Contackt : BaseModel
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
    }
}
