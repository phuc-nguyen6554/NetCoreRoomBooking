using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        public BookingServiceContext(DbContextOptions options) : base(options) { }

        //public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);         
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            //builder.UseLoggerFactory(loggerFactory);
        }
    }
}
