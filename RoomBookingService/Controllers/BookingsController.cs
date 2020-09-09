using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoomBookingService.Models;
using RoomBookingService.Models.Bookings;

namespace RoomBookingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly BookingServiceContext _context;

        public BookingsController(BookingServiceContext context)
        {
            _context = context;
        }

        // GET: api/Bookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> Getbookings()
        {
            // Get Upcomming Booking event
            return await _context.bookings
                .Include(b => b.Room)
                .Where(b => b.To.CompareTo(DateTime.Now) > 0)
                .OrderBy(b => b.From)
                .ToListAsync();
        }

        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(int id)
        {
            var booking = await _context.bookings.FindAsync(id);

            if (booking == null)
            {
                return NotFound();
            }

            return booking;
        }

        // POST: api/Bookings
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Booking>> PostBooking(Booking booking)
        {           
            var ExistedBookings = await _context.bookings
                .Where(b => b.RoomID == booking.RoomID &&
                    ((b.From.CompareTo(booking.From) <= 0 && b.To.CompareTo(booking.From) >= 0) ||
                    (b.From.CompareTo(booking.To) <= 0 && b.To.CompareTo(booking.To) >= 0)))
                .ToListAsync();

            if(ExistedBookings.Count > 0)
            {
                return BadRequest("Time is not correct");
            }

            string username = Request.Headers["X-Forwarded-Username"];
            string email = Request.Headers["X-Forwarded-Email"];

            booking.MemberName = username;
            booking.MemberEmail = email;

            _context.bookings.Add(booking);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBooking", new { id = booking.Id }, booking);
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Booking>> DeleteBooking(int id)
        {
            var booking = await _context.bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            string email = Request.Headers["X-Forwarded-Email"];

            if (booking.MemberEmail != email)
            {
                return Unauthorized("You don't have permission to delete this booking");
            }

            _context.bookings.Remove(booking);
            await _context.SaveChangesAsync();

            return booking;
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            string username = Request.Headers["X-Forwarded-Username"];
            return Ok(new { Username = username, Header = Request.Headers});
        }

        private bool BookingExists(int id)
        {
            return _context.bookings.Any(e => e.Id == id);
        }
    }
}
