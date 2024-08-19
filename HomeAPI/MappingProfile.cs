using AutoMapper;
using HomeAPI.Configuration;
using HomeAPI.Contracts.Models.Devices;
using HomeAPI.Contracts.Models.Home;
using HomeAPI.Contracts.Models.Rooms;
using HomeAPI.Data.Models;

namespace HomeAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Address, AddressInfo>();
            CreateMap<HomeOptions, InfoResponse>()
                .ForMember(m => m.AddressInfo,
                    opt => opt.MapFrom(src => src.Address));
            
            CreateMap<AddDeviceRequest, Device>()
                .ForMember(d => d.Location,
                    opt => opt.MapFrom(r => r.RoomLocation));
            CreateMap<AddRoomRequest, Room>();
            CreateMap<Device, DeviceView>();
        }
    }
}