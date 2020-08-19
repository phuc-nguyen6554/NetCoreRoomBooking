using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
                .Where(b => b.From.CompareTo(DateTime.Now) > 0)
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
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Booking>> PostBooking(Booking booking)
        {           
            var ExistedBookings = await _context.bookings
                .Where(b => 
                    (b.From.CompareTo(booking.From) <= 0 && b.To.CompareTo(booking.From) >= 0) || 
                    (b.From.CompareTo(booking.To) <= 0 && b.To.CompareTo(booking.To) >= 0))
                .ToListAsync();

            if(ExistedBookings.Count > 0)
            {
                return BadRequest("Time is not correct");
            }

            // Get info from JWT
            string username = User.Claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault()?.Value;
            string email = User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault()?.Value;

            booking.MemberName = username;
            booking.MemberEmail = email;

            _context.bookings.Add(booking);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBooking", new { id = booking.Id }, booking);
        }

        [HttpGet("Test")]
        public async Task<IEnumerable<Booking>> GetData()
        {
            DateTime From = DateTime.Parse("2020-8-19 15:15");
            DateTime To = DateTime.Parse("2020-8-19 15:30");

            var bookings = await _context.bookings.Where(b => (b.From.CompareTo(From) <= 0 && b.To.CompareTo(From) >= 0) || (b.From.CompareTo(To) <= 0 && b.To.CompareTo(To) >= 0))
                .ToListAsync();

            return bookings;
        }

        // DELETE: api/Bookings/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Booking>> DeleteBooking(int id)
        {
            var booking = await _context.bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            _context.bookings.Remove(booking);
            await _context.SaveChangesAsync();

            return booking;
        }

        private bool BookingExists(int id)
        {
            return _context.bookings.Any(e => e.Id == id);
        }
    }
}
