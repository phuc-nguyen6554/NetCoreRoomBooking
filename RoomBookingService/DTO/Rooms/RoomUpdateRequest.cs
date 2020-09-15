using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomBookingService.DTO.Rooms
{
    public class RoomUpdateRequest
    {
        public int Id { get; set; }
        public string RoomName { get; set; }
    }
}
