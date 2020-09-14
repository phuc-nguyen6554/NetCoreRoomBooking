using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoomBookingService.Models;
using RoomBookingService.Models.Bookings;
using AutoMapper;
using RoomBookingService.DTO.Bookings;
using RoomBookingService.Services;
using Shared;

namespace RoomBookingService.Controllers
{
    [Route("bookings")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<BookingListResponse>), 200)]
        public async Task<IActionResult> GetBookingAsync(BookingListRequest request)
        {
            return Ok(await _bookingService.GetBookingAsync(request));
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BookingDetailResponse), 200)]
        public async Task<IActionResult> GetBookingDetailAsync(int id)
        {
            return Ok(await _bookingService.GetBookingDetailAsync(id));
        }

        [HttpPost]
        [ProducesResponseType(typeof(BookingCreateResponse), 200)]
        public async Task<IActionResult> CreateBookingAsync([FromBody] BookingCreateRequest request)
        {
            if (!ModelState.IsValid)
                throw new ServiceException(400, "Model is invalid");

            return Ok(await _bookingService.CreateBookingAsync(request));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(BookingUpdateResponse), 200)]
        public async Task<IActionResult> UpdateBookingAsync(int id, [FromBody] BookingUpdateRequest request)
        {
            if (!ModelState.IsValid)
                throw new ServiceException(400, "Model is invalid");

            return Ok(await _bookingService.UpdateBookingAsync(id, request));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteBookingAsync(int id)
        {
            await _bookingService.DeleteBookingAsync(id);
            return Ok();
        }



        //// GET: api/Bookings
        //[HttpGet()]
        //public async Task<ActionResult<IEnumerable<BookingResponse>>> GetBookings()
        //{
        //    return Ok(await _bookingService.GetBookingsAsync());
        //}

        //// GET: api/Bookings/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<BookingResponse>> GetBooking(int id)
        //{
        //    var booking = await _context.bookings.FindAsync(id);

        //    if (booking == null)
        //    {
        //        return NotFound();
        //    }

        //    var result = _mapper.Map<BookingResponse>(booking);

        //    return result;
        //}

        //// POST: api/Bookings
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<Booking>> PostBooking(Booking booking)
        //{
        //    var ExistedBookings = await _context.bookings
        //        .Where(b => b.RoomID == booking.RoomID &&
        //            ((b.From.CompareTo(booking.From) <= 0 && b.To.CompareTo(booking.From) >= 0) ||
        //            (b.From.CompareTo(booking.To) <= 0 && b.To.CompareTo(booking.To) >= 0)))
        //        .ToListAsync();

        //    if (ExistedBookings.Count > 0)
        //    {
        //        return BadRequest("Time is not correct");
        //    }

        //    string username = Request.Headers["X-Forwarded-Username"];
        //    string email = Request.Headers["X-Forwarded-Email"];

        //    booking.MemberName = username;
        //    booking.MemberEmail = email;

        //    _context.bookings.Add(booking);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetBooking", new { id = booking.Id }, booking);
        //}

        //// DELETE: api/Bookings/5?filter=a
        //[HttpDelete("{id}/{id234}")]
        //public async Task<ActionResult<BookingResponse>> DeleteBooking(int id, string id234)
        //{
        //    var booking = await _context.bookings.FindAsync(id);
        //    if (booking == null)
        //    {
        //        return NotFound();
        //    }

        //    string email = Request.Headers["X-Forwarded-Email"];

        //    if (booking.MemberEmail != email)
        //    {
        //        return Unauthorized("You don't have permission to delete this booking");
        //    }

        //    _context.bookings.Remove(booking);
        //    await _context.SaveChangesAsync();

        //    var result = _mapper.Map<BookingResponse>(booking);
        //    return result;
        //}

        //private bool BookingExists(int id)
        //{
        //    return _context.bookings.Any(e => e.Id == id);
        //}
    }
}
