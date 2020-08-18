using RoomBookingService.Models.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomBookingService.Models.Bookings
{
    public class Booking
    {
        public int Id { get; set; }

        public int RoomID { get; set; }
        public Room Room { get; set; }

        public string MemberName { get; set; }
        public string MemberEmail { get; set; }

        public DateTime BookTime { get; set; }
    }
}
