using RoomBookingService.Models.Rooms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RoomBookingService.Models.Bookings
{
    [Table("BR_Bookings")]
    public class Booking
    {
       
        public int Id { get; set; }

        public int RoomId { get; set; }

        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }

        public string MemberName { get; set; }
        public string MemberEmail { get; set; }

        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public string Description { get; set; }
    }
}
