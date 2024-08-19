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
    }
}