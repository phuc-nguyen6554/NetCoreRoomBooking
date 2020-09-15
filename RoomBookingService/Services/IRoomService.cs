using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoomBookingService.DTO.Rooms;

namespace RoomBookingService.Services
{
    public interface IRoomService
    {
        public Task<List<RoomListResponse>> GetRoomListAsync();
        public Task<RoomDetailResponse> GetRoomDetailAsync(int id);
        public Task<RoomCreateResponse> CreateRoomAsync(RoomCreateRequest request);
        public Task<RoomUpdateResponse> UpdateRoomAsync(int id, RoomUpdateRequest request);
        public Task DeleteRoomAsync(int id);
    }
}
