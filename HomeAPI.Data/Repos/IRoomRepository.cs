using HomeAPI.Data.Models;

namespace HomeAPI.Data.Repos
{
    public interface IRoomRepository
    {
        Task<Room> GetRoomByName(string name);
        Task<Room> GetRoomById(Guid Id);
        Task AddRoom(Room room);
        Task<Room[]> GetRooms();
        Task UpdateRoom(Room room);
    }
}