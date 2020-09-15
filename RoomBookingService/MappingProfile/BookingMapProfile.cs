using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoomBookingService.Models.Bookings;
using RoomBookingService.DTO.Bookings;

namespace RoomBookingService.MappingProfile
{
    public class BookingMapProfile : Profile
    {
        public BookingMapProfile()
        {
            CreateMap<Booking, BookingListResponse>();
            CreateMap<Booking, BookingDetailResponse>();
            CreateMap<Booking, BookingCreateResponse>();
            CreateMap<Booking, BookingUpdateResponse>();
            CreateMap<BookingCreateRequest, Booking>();
            CreateMap<BookingUpdateRequest, Booking>();
        }
    }
}
