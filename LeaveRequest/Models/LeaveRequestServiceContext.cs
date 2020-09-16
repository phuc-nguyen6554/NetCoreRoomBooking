using Microsoft.EntityFrameworkCore;
using LeaveRequest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.Models
{
    public class LeaveRequestServiceContext : DbContext
    {
        public LeaveRequestServiceContext(DbContextOptions options) :base(options) { }
        public DbSet<Request> LeaveRequests { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<Booking>().HasOne(b => b.Room).WithMany(r => r.Bookings);
        }
    }
}
