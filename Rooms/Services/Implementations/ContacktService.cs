using Rooms.Context;
using Rooms.Models;
using Rooms.Services.Interfaces;

namespace Rooms.Services.Implementations
{
    public class ContacktService : IContactService
    {
        private readonly ApplicationDbContext _db;
        public ContacktService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Contackt> Create(Contackt contackt)
        {
            Contackt con = new Contackt()
            {
                Email = contackt.Email,
                CreatedAt = DateTime.Now,
                Message = contackt.Message,
                Name = contackt.Name,
                Subject = contackt.Subject,
            };
            await _db.Contackts.AddAsync(con);
             _db.SaveChanges();
            return con;
        }

        public ICollection<Contackt> GetOrders()
        {
            var data = _db.Contackts.Where(x => !x.IsDeleted).ToList();
            return data;
        }

        public async Task<Contackt> Remove(int id)
        {
            var rem = _db.Contackts.SingleOrDefault(x => x.Id == id);
            rem.IsDeleted = true;
            await _db.SaveChangesAsync();
            return rem;
        }
    }
}
