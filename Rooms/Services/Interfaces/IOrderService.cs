using Rooms.Models;
using Rooms.ViewModels;

namespace Rooms.Services.Interfaces
{
    public interface IOrderService 
    {
        Task<Bookeds> Create(CreateBookingViewModel order , string username);
        ICollection<Bookeds> GetOrders();
        Task<Bookeds> Remove(int id);
    }
}
