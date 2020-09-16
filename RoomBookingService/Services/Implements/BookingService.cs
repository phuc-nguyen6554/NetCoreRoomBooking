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
using Shared.Exceptions;

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

        public async Task<List<BookingListResponse>> GetBookingAsync()
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
            if (await IsDuplicate(request.RoomId, request.From, request.To))
                throw new ServiceException(400, "Duplicate Entry");

            var booking = _mapper.Map<Booking>(request);

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
           
            return _mapper.Map<BookingCreateResponse>(booking);
        }

        public async Task<BookingUpdateResponse> UpdateBookingAsync(int id, BookingUpdateRequest request)
        {
            if (id != request.Id)
                throw new Exception("Bad Request");

            if(await IsDuplicate(request.RoomId, request.From, request.To, true, request.Id))
                throw new ServiceException(400, "Duplicate Entry");

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

        private async Task<bool> IsDuplicate(int roomID, DateTime from, DateTime to, bool isUpdate = false, int id = -1)
        {
            var ExistedBookings =await _context.Bookings
                .Where(b => b.RoomId == roomID &&
                    ((b.From.CompareTo(from) <= 0 && b.To.CompareTo(from) >= 0) ||
                    (b.From.CompareTo(to) <= 0 && b.To.CompareTo(to) >= 0)))
                .ToListAsync();

            if(isUpdate)
            {
                var booking = ExistedBookings.Where(x => x.Id != id).ToList();
                return booking.Count > 0;
            }

            return ExistedBookings.Count > 0;
        }

    }
}
