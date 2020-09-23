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
using Shared.Exceptions;
using Shared.Mail;

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
        public async Task<IActionResult> GetBookingAsync(/*[FromBody] BookingListRequest request*/)
        {
            await MailService.SendMail("phucthanh6554@gmail.com", "Test Mail Service", "Hello tui ne");
            return Ok(await _bookingService.GetBookingAsync());
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

            //request.MemberName = Request.Headers["X-Forwarded-Username"];
            //request.MemberEmail = Request.Headers["X-Forwarded-Email"];

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
    }
}
