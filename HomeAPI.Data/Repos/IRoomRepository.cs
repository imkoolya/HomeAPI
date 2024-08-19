using HomeAPI.Data.Models;

namespace HomeAPI.Data.Repos
{
    public interface IRoomRepository
    {
        Task<Room> GetRoomByName(string name);
        Task AddRoom(Room room);
        Task<Room[]> GetRooms();
    }
}