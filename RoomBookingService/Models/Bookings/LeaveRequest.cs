using RoomBookingService.Models.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomBookingService.Models.Bookings
{
    public class LeaveRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime LeaveDates { get; set; }

        public String LeaveTime { get; set; }

        public int LeaveTypeId { get; set; }

        public String Reason { get; set; }

    }
}
