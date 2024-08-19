using HomeAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeAPI.Data.Repos
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HomeApiContext _context;
        
        public RoomRepository (HomeApiContext context)
        {
            _context = context;
        }

        public async Task<Room> GetRoomByName(string name)
        {
            return await _context.Rooms.Where(r => r.Name == name).FirstOrDefaultAsync();
        }

        public async Task<Room> GetRoomById(Guid id)
        {
            return await _context.Rooms.Where(r => r.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddRoom(Room room)
        {
            var entry = _context.Entry(room);
            if (entry.State == EntityState.Detached)
                await _context.Rooms.AddAsync(room);
            
            await _context.SaveChangesAsync();
        }

        public async Task<Room[]> GetRooms()
        {
            return await _context.Rooms
                .Include(r => r.Name)
                .ToArrayAsync();
        }

        public async Task UpdateRoom(Room room)
        {
            Room roomUpdate = _context.Rooms.Where(r => r.Id == room.Id).FirstOrDefault();
            roomUpdate.Name = room.Name;
            roomUpdate.Area = room.Area;
            roomUpdate.GasConnected = room.GasConnected;
            roomUpdate.Voltage = room.Voltage;

            var entry = _context.Entry(roomUpdate);
            if (entry.State == EntityState.Detached)
                _context.Rooms.Update(roomUpdate);
            await _context.SaveChangesAsync();
        }
    }
}