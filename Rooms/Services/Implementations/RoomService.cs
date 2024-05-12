using Rooms.Context;
using Rooms.Models;
using Rooms.Services.Interfaces;
using Rooms.ViewModels;

namespace Rooms.Services.Implementations
{
    public class RoomService : IRoomService
    {
        private readonly ApplicationDbContext _db;
        public RoomService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Room> Create(CreateRoomViewModel room)
        {
            Room rooms = new Room()
            {
                CreatedAt = DateTime.Now,
                Name = room.Name,
                Description = room.Description,
            };

            await _db.Rooms.AddAsync(rooms);
            await _db.SaveChangesAsync();

            foreach (var item in room.RoomPhotos)
            {
                var uploadDirectory = Path.Combine("wwwroot", "img");

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(item.FileName);
                var saveFilePath = Path.Combine(uploadDirectory, fileName);

                using (var stream = new FileStream(saveFilePath, FileMode.Create))
                {
                    await item.CopyToAsync(stream);
                }

                var roomPhotoEntity = new RoomPhotos()
                {
                    RoomId = rooms.Id,
                    CreatedAt = DateTime.Now,
                    PhotoName = fileName,
                };

                await _db.Photos.AddAsync(roomPhotoEntity);
            }

            await _db.SaveChangesAsync();

            return rooms;

        }
        public async Task<Room> Delete(int id)
        {
            var remove = _db.Rooms.SingleOrDefault(x => x.Id == id);
            remove.IsDeleted = true;
            var rem = _db.Photos.Where(x => x.RoomId == id).ToList();
            foreach (var item in rem)
            {
                item.IsDeleted = true;

                var r = _db.Comments.Where(x => x.RoomId == id).ToList();
                foreach (var items in r)
                {
                    items.IsDeleted = true;
                }
            }
            await _db.SaveChangesAsync();
            return remove;
        }

        public async Task<Room> Get(int id)
        {
            var data = _db.Rooms.SingleOrDefault(x => x.Id == id);
            data.RoomPhotos = _db.Photos.Where(x => x.RoomId == id).ToList();
            data.Comment = _db.Comments.Where(x => x.RoomId == id).ToList();
            return data;
        }

        public List<Room> GetAll()
        {
            var data = _db.Rooms.Where(x => !x.IsDeleted).ToList();

            foreach (var item in data)
            {
                item.RoomPhotos = _db.Photos.Where(x => x.RoomId == item.Id).ToList();
            }
            foreach (var item in data)
            {
                item.Comment = _db.Comments.Where(x => x.RoomId == item.Id).ToList();
            }
            return data;
        }
        public List<Room> GetByPage(int page)
        {
            const int pageSize = 3; 
            var skipAmount = page * pageSize;

            var data = _db.Rooms
                            .Where(x => !x.IsDeleted)
                            .Skip(skipAmount)
                            .Take(pageSize)
                            .ToList();

            foreach (var item in data)
            {
                item.RoomPhotos = _db.Photos.Where(x => x.RoomId == item.Id).ToList();
                item.Comment = _db.Comments.Where(x => x.RoomId == item.Id).ToList();
            }

            return data;
        }

        public async Task<Room> Update(Room room)
        {
            var data = _db.Rooms.SingleOrDefault(x => x.Id == room.Id);
            data.Name = room.Name;
            data.Description = room.Description;
            data.UpdatedAt = DateTime.Now;
            _db.Rooms.Update(data);
            await _db.SaveChangesAsync();
            return data;
        }
    }
}
