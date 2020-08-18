using RoomBookingService.Models.Bookings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomBookingService.Models.Rooms
{
    public class Room
    {
        public int Id { get; set; }
        public string RoomName {get;set;}

        public List<Booking> Bookings { get; set; }
    }
}
