﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBookingService.DTO.Bookings
{
    public class BookingUpdateRequest
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string MemberName { get; set; }
        public string MemberEmail { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
