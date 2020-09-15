using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoomBookingService.DTO.Bookings;

namespace RoomBookingService.Services
{
    public interface IBookingService
    {
        Task<List<BookingListResponse>> GetBookingAsync(BookingListRequest request);

        Task<BookingDetailResponse> GetBookingDetailAsync(int id);

        Task<BookingCreateResponse> CreateBookingAsync(BookingCreateRequest request);

        Task<BookingUpdateResponse> UpdateBookingAsync(int id, BookingUpdateRequest request);

        Task DeleteBookingAsync(int id);
    }
}
