using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RoomBookingService.DTO.Rooms;
using RoomBookingService.Models;
using RoomBookingService.Models.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomBookingService.Services.Implements
{
    public class RoomService : IRoomService
    {
        public BookingServiceContext _context;
        public IMapper _mapper;

        public RoomService(BookingServiceContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<RoomCreateResponse> CreateRoomAsync(RoomCreateRequest request)
        {
            var model = _mapper.Map<Room>(request);
            _context.Rooms.Add(model);
            await _context.SaveChangesAsync();

            return _mapper.Map<RoomCreateResponse>(model);
        }

        public async Task DeleteRoomAsync(int id)
        {
            var model = await _context.Rooms.FindAsync(id);

            if (model == null)
                throw new Exception("Not Found");

            _context.Rooms.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<RoomDetailResponse> GetRoomDetailAsync(int id)
        {
            var model = await _context.Rooms.FindAsync(id);

            if (model == null)
                throw new Exception("Not Found");

            return _mapper.Map<RoomDetailResponse>(model);
        }

        public async Task<List<RoomListResponse>> GetRoomListAsync()
        {
            var bookings = await _context.Rooms.ToListAsync();
            return _mapper.Map<List<RoomListResponse>>(bookings);
        }

        public async Task<RoomUpdateResponse> UpdateRoomAsync(int id, RoomUpdateRequest request)
        {
            if (id != request.Id)
                throw new Exception("Bad Request");

            var model = _mapper.Map<Room>(request);
            _context.Entry(model).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return _mapper.Map<RoomUpdateResponse>(model);
        }
    }
}
