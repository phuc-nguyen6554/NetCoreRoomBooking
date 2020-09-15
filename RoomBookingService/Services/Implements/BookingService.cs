using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RoomBookingService.DTO.Bookings;
using RoomBookingService.Models;
using RoomBookingService.Models.Bookings;
using Shared;

namespace RoomBookingService.Services.Implements
{
    public class BookingService : IBookingService
    {
        private readonly BookingServiceContext _context;
        private readonly IMapper _mapper;

        public BookingService(BookingServiceContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<BookingResponse>> GetBookingsAsync()
        {
            var booking = await _context.Bookings
                .Include(b => b.Room)
                .Where(b => b.To.CompareTo(DateTime.Now) > 0)
                .OrderBy(b => b.From)
                .ToListAsync();

            return _mapper.Map<List<BookingResponse>>(booking);
        }

        public async Task<List<BookingListResponse>> GetBookingAsync(BookingListRequest request)
        {
            var bookings = await _context.Bookings
                .Include(b => b.Room)
                .Where(b => b.To.CompareTo(DateTime.Now) > 0)
                .OrderBy(b => b.From)
                .ToListAsync();

            return _mapper.Map<List<BookingListResponse>>(bookings);
        }

        public async Task<BookingDetailResponse> GetBookingDetailAsync(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            return _mapper.Map<BookingDetailResponse>(booking);
        }

        public async Task<BookingCreateResponse> CreateBookingAsync(BookingCreateRequest request)
        {
            var ExistedBookings = await _context.Bookings
                .Where(b => b.RoomId == request.RoomId &&
                    ((b.From.CompareTo(request.From) <= 0 && b.To.CompareTo(request.From) >= 0) ||
                    (b.From.CompareTo(request.To) <= 0 && b.To.CompareTo(request.To) >= 0)))
                .ToListAsync();

            if (ExistedBookings.Count > 0)
                throw new Exception("Duplicate Item");

            var booking = _mapper.Map<Booking>(request);

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
           
            return _mapper.Map<BookingCreateResponse>(booking);
        }

        public async Task<BookingUpdateResponse> UpdateBookingAsync(int id, BookingUpdateRequest request)
        {
            if (id != request.Id)
                throw new Exception("Bad Request");

            var booking = _mapper.Map<Booking>(request);

            _context.Entry(booking).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return _mapper.Map<BookingUpdateResponse>(booking);
        }

        public async Task DeleteBookingAsync(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
        }

    }
}
