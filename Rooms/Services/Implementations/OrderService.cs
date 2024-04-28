using Rooms.Context;
using Rooms.Models;
using Rooms.Services.Interfaces;
using Rooms.ViewModels;
using System.Web.Mvc;

namespace Rooms.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _db;
        public OrderService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Bookeds> Create(CreateBookingViewModel order)
        {
                Bookeds data = new Bookeds()
                {
                    CreatedAt = DateTime.Now,
                    RoomId = order.RoomId,
                    CheckIn = order.CheckIn,
                    CheckOut = order.CheckOut,
                    UserId = order.UserId,
                    Request = order.Request,
                };
                await _db.Orders.AddAsync(data);
                _db.SaveChanges();
                return data;
        }

        public ICollection<Bookeds> GetByUser(int userId)
        {
            var ord = _db.Orders.Where(x => x.Id == userId).ToList();
            return ord;
        }

        public ICollection<Bookeds> GetOrders(int pg = 1)
        {
            var ord = _db.Orders.Where(x => !x.IsDeleted).ToList();
            return ord;
        }

        public async Task<Bookeds> Remove(int id)
        {
            var rem = _db.Orders.SingleOrDefault(x => x.Id == id);
            rem.IsDeleted = true;
            await _db.SaveChangesAsync();
            return rem;
        }
    }
}
