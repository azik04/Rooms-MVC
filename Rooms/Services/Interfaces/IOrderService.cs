using Rooms.Models;
using Rooms.ViewModels;

namespace Rooms.Services.Interfaces
{
    public interface IOrderService 
    {
        Task<Bookeds> Create(CreateBookingViewModel order);
        ICollection<Bookeds> GetOrders();
        ICollection<Bookeds> GetByUser(int userId);
        Task<Bookeds> Remove(int id);
    }
}
