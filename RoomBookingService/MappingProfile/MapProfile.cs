using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RoomBookingService.DTO.Bookings;
using RoomBookingService.Models.Bookings;

namespace RoomBookingService.MappingProfile
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Booking, BookingResponse>();
        }
    }
}
