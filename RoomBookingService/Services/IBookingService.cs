﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoomBookingService.DTO.Bookings;
using Shared.Data;

namespace RoomBookingService.Services
{
    public interface IBookingService
    {
        Task<List<BookingListResponse>> GetBookingAsync(/*BookingListRequest request*/);

        Task<PagedListResponse<BookingListResponse>> GetBookingPagedAsync(PagedListRequest request);

        Task<BookingDetailResponse> GetBookingDetailAsync(int id);

        Task<BookingCreateResponse> CreateBookingAsync(BookingCreateRequest request);

        Task<BookingUpdateResponse> UpdateBookingAsync(int id, BookingUpdateRequest request);

        Task DeleteBookingAsync(int id);
    }
}
