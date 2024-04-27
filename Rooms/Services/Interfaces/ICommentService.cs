using Rooms.Models;

namespace Rooms.Services.Interfaces
{
    public interface ICommentService
    {
        ICollection<Comments> GetByRoom(int id);
        ICollection<Comments> GetAll();
        Task<Comments> CreateComment(Comments comments);
    }
}
