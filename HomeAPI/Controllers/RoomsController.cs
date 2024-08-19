using AutoMapper;
using HomeAPI.Contracts.Models.Rooms;
using HomeAPI.Data.Models;
using HomeAPI.Data.Repos;
using Microsoft.AspNetCore.Mvc;

namespace HomeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomsController : ControllerBase
    {
        private IRoomRepository _repository;
        private IMapper _mapper;
        
        public RoomsController(IRoomRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IActionResult> GetRooms()
        {
            var rooms = await _repository.GetRooms();

            var resp = new GetRoomsResponse
            {
                RoomAmount = rooms.Length,
                Rooms = _mapper.Map<Room[], RoomView[]>(rooms)
            };

            return StatusCode(200, resp);
        }

        [HttpPost] 
        [Route("")] 
        public async Task<IActionResult> Add([FromBody] AddRoomRequest request)
        {
            var existingRoom = await _repository.GetRoomByName(request.Name);
            if (existingRoom == null)
            {
                var newRoom = _mapper.Map<AddRoomRequest, Room>(request);
                await _repository.AddRoom(newRoom);
                return StatusCode(201, $"Комната {request.Name} добавлена!");
            }
            
            return StatusCode(409, $"Ошибка: Комната {request.Name} уже существует.");
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Edit([FromRoute] Guid id,[FromBody] EditRoomRequest request)
        {
            var editingRoom = await _repository.GetRoomById(id);

            if (editingRoom == null)
            {
                return StatusCode(400, $"Комната с id: {id} не найдена");
            }

            Room editedRoom = new Room
            {
                Id = id,
                AddDate = editingRoom.AddDate,
                Area = request.NewArea,
                GasConnected = request.NewGasConnected,
                Name = request.NewName,
                Voltage = request.NewVoltage
            };

            await _repository.UpdateRoom(editedRoom);

            return StatusCode(200, $"Комната обновлена! Id - {editedRoom.Id}, Название = {editedRoom.Name}, Площадь = {editedRoom.Area}, Наличие газа = {editedRoom.GasConnected}, Напряжение = {editedRoom.Voltage}");
        }
    }
}