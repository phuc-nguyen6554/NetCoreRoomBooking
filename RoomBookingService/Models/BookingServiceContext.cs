using Microsoft.EntityFrameworkCore;
using RoomBookingService.Models.Bookings;
using RoomBookingService.Models.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomBookingService.Models
{
    public class BookingServiceContext : DbContext
    {
        public BookingServiceContext(DbContextOptions options) :base(options) { }

        public DbSet<Room> rooms { get; set; }
        public DbSet<Booking> bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Booking>().HasOne(b => b.Room).WithMany(r => r.Bookings);
        }
    }
}
