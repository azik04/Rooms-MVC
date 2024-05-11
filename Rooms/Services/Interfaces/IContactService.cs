using Rooms.Models;
using Rooms.ViewModels;

namespace Rooms.Services.Interfaces
{
    public interface IContactService
    {
        Task<Contackt> Create(Contackt contackt);
        ICollection<Contackt> GetOrders();
        Task<Contackt> Remove(int id);
    }
}
