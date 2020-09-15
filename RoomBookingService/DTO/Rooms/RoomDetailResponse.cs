using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoomBookingService.Models.Bookings;

namespace RoomBookingService.DTO.Rooms
{
    public class RoomDetailResponse
    {
        public int Id { get; set; }
        public string RoomName { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
