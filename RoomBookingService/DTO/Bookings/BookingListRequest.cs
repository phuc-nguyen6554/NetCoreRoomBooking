using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBookingService.DTO.Bookings
{
    public class BookingListRequest
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
