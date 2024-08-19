using AutoMapper;
using HomeAPI.Contracts.Models.Devices;
using HomeAPI.Data.Models;
using HomeAPI.Data.Queries;
using HomeAPI.Data.Repos;
using Microsoft.AspNetCore.Mvc;

namespace HomeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DevicesController : ControllerBase
    {
        private DeviceRepository _devices;
        private IRoomRepository _rooms;
        private IMapper _mapper;
        
        public DevicesController(IDeviceRepository devices, IRoomRepository rooms, IMapper mapper)
        {
            _devices = (DeviceRepository?)devices;
            _rooms = rooms;
            _mapper = mapper;
        }

        [HttpGet] 
        [Route("")] 
        public async Task<IActionResult> GetDevices()
        {
            var devices = await _devices.GetDevices();

            var resp = new GetDevicesResponse
            {
                DeviceAmount = devices.Length,
                Devices = _mapper.Map<Device[], DeviceView[]>(devices)
            };
            
            return StatusCode(200, resp);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Delete(AddDeviceRequest request)
        {
            var room = await _rooms.GetRoomByName(request.RoomLocation);
            if (room == null)
                return StatusCode(400, $"Ошибка: Комната {request.RoomLocation} не подключена. Сначала подключите комнату!");

            var device = await _devices.GetDeviceByName(request.Name);
            if (device != null)
                return StatusCode(400, $"Ошибка: Устройство {request.Name} уже существует.");

            var removeDevice = _mapper.Map<AddDeviceRequest, Device>(request);
            await _devices.DeleteDevice(removeDevice);

            return StatusCode(201, $"Устройство {request.Name} удалено.");
        }

        [HttpPost] 
        [Route("")] 
        public async Task<IActionResult> Add( AddDeviceRequest request )
        {
            var room = await _rooms.GetRoomByName(request.RoomLocation);
            if(room == null)
                return StatusCode(400, $"Ошибка: Комната {request.RoomLocation} не подключена. Сначала подключите комнату!");
            
            var device = await _devices.GetDeviceByName(request.Name);
            if(device != null)
                return StatusCode(400, $"Ошибка: Устройство {request.Name} уже существует.");
            
            var newDevice = _mapper.Map<AddDeviceRequest, Device>(request);
            await _devices.SaveDevice(newDevice, room);
            
            return StatusCode(201, $"Устройство {request.Name} добавлено. Идентификатор: {newDevice.Id}");
        }

        [HttpPatch] 
        [Route("{id}")] 
        public async Task<IActionResult> Edit(
            [FromRoute] Guid id,
            [FromBody]  EditDeviceRequest request)
        {
            var room = await _rooms.GetRoomByName(request.NewRoom);
            if(room == null)
                return StatusCode(400, $"Ошибка: Комната {request.NewRoom} не подключена. Сначала подключите комнату!");
            
            var device = await _devices.GetDeviceById(id);
            if(device == null)
                return StatusCode(400, $"Ошибка: Устройство с идентификатором {id} не существует.");
            
            var withSameName = await _devices.GetDeviceByName(request.NewName);
            if(withSameName != null)
                return StatusCode(400, $"Ошибка: Устройство с именем {request.NewName} уже подключено. Выберите другое имя!");

            await _devices.UpdateDevice(
                device,
                room,
                new UpdateDeviceQuery(request.NewName, request.NewSerial)
            );

            return StatusCode(200, $"Устройство обновлено! Имя - {device.Name}, Серийный номер - {device.SerialNumber},  Комната подключения - {device.Room.Name}");
        }
    }
}