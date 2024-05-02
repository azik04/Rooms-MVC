using Rooms.Models;
using Rooms.ViewModels;

namespace Rooms.Services.Interfaces
{
    public interface IRoomService
    {
        Task<Room> Create(CreateRoomViewModel room);
        Task<Room> Update(Room room);
        Task<Room> Delete(int id);
        Task<Room> Get(int id);
        List<Room> GetAll();
        List<Room> GetByPage(int pageNumber, int pageSize);
    }
}
