using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoomBookingService.Models.Rooms;
using RoomBookingService.DTO.Rooms;

namespace RoomBookingService.MappingProfile
{
    public class RoomMapProfile : Profile
    {
        public RoomMapProfile()
        {
            CreateMap<Room, RoomCreateResponse>();
            CreateMap<Room, RoomUpdateResponse>();
            CreateMap<Room, RoomDetailResponse>();
            CreateMap<Room, RoomListResponse>();
            CreateMap<RoomCreateRequest, Room>();
            CreateMap<RoomUpdateRequest, Room>();
        }
    }
}
