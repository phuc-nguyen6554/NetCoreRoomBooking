using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RoomBookingService.DTO.Bookings;
using RoomBookingService.Models;
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
            throw new NotImplementedException();
        }

        public async Task<BookingListResponse> GetBookingDetailAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<BookingCreateResponse> CreateBookingAsync(BookingCreateRequest request)
        {
            var booking = await _context.Bookings
                .Include(inc => inc.Room)
                .FirstOrDefaultAsync();

            var validate = false;

            if(!validate)
                throw new ServiceException(400, "Model invalid");

            var isExists = false;
            if (!isExists)
                throw new ServiceException(404, "Model not found");

            throw new NotImplementedException();
        }

        public async Task<BookingUpdateResponse> UpdateBookingAsync(int id, BookingUpdateRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteBookingAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
