using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomBookingService.DTO.Bookings
{
    public class BookingResponse
    {
        public int ID { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string MemberName { get; set; }
        public string MemberEmail { get; set; }
        public string RoomName { get; set; }
    }
}
