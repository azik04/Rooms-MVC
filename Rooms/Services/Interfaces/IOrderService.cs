using Rooms.Models;
using Rooms.ViewModels;

namespace Rooms.Services.Interfaces
{
    public interface IOrderService 
    {
        Task<Bookeds> Create(CreateBookingViewModel order , string username);
        ICollection<Bookeds> GetOrders();
        //ICollection<Bookeds> GetByUser(string userName);
        Task<Bookeds> Remove(int id);
    }
}
