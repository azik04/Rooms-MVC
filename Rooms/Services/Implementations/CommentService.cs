using Rooms.Context;
using Rooms.Models;
using Rooms.Services.Interfaces;

namespace Rooms.Services.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext _db;
        public CommentService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Comments> CreateComment(Comments comments)
        {
            Comments com = new Comments()
            {
                Body = comments.Body,
                CreatedAt = DateTime.Now,
                RoomId = comments.RoomId,
            };
            await _db.Comments.AddAsync(com);
            await _db.SaveChangesAsync();
            return com;
        }

        public ICollection<Comments> GetAll()
        {
            var data = _db.Comments.ToList();
            return data;
        }

        public ICollection<Comments> GetByRoom(int id)
        {
            var data = _db.Comments.Where(x => x.RoomId == id).ToList();
            return data;
        }
    }
}
