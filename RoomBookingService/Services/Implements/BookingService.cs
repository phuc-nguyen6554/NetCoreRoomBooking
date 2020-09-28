using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RoomBookingService.DTO.Bookings;
using RoomBookingService.Models;
using RoomBookingService.Models.Bookings;
using Shared.Cache;
using Shared.Exceptions;
using Shared.Data;
using Shared.Extensions;
using Shared.RabbitQueue;
using Shared.HttpService;
using MailService.MailServiceHttp;
using MailService.DTO;
using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;

namespace RoomBookingService.Services.Implements
{
    public class BookingService : IBookingService
    {
        private readonly BookingServiceContext _context;
        private readonly IMapper _mapper;
        private readonly IScopedCache _cache;
        private readonly IMailHttp _mail;

        public BookingService(BookingServiceContext context, IMapper mapper, IScopedCache cache, IMailHttp mail)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
            _mail = mail;
        }

        public async Task<List<BookingListResponse>> GetBookingAsync(/*BookingListRequest request*/)
        {
            var bookings = await _context.Bookings
                .Include(b => b.Room)
                .Where(b => b.To.CompareTo(DateTime.Now) > 0)
                .OrderBy(b => b.From)
                .ToListAsync();
            return _mapper.Map<List<BookingListResponse>>(bookings);
        }

        public async Task<PagedListResponse<BookingListResponse>> GetBookingPagedAsync(PagedListRequest request)
        {

            var bookings = await _context.Bookings
                .Include(b => b.Room)
                .Where(b => b.To.CompareTo(DateTime.Now) > 0)
                .OrderBy(b => b.From)
                .ToPagedList(request.Page, request.PageSize);

            var bookingResult = _mapper.Map<List<BookingListResponse>>(bookings.Result);
            return bookings.Map(bookingResult);
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
            booking.MemberName = _cache.Username;
            booking.MemberEmail = _cache.Email;

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

            if(booking.MemberEmail != _cache.Email && _cache.Role != Constrain.AdminRole)
            {
                throw new ServiceException(401, "You don't have permission to delete this Booking");
            }
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
        }

        private async Task<bool> IsDuplicate(int roomID, DateTime from, DateTime to, bool isUpdate = false, int id = -1)
        {
            var ExistedBookings =await _context.Bookings
                .Where(b => b.RoomId == roomID &&
                    ((b.From.CompareTo(from) <= 0 && b.To.CompareTo(from) >= 0) ||
                    (b.From.CompareTo(to) <= 0 && b.To.CompareTo(to) >= 0) ||
                    (b.From.CompareTo(from) >= 0 && b.To.CompareTo(to) <= 0)))
                .AsNoTracking()
                .ToListAsync();

            if(isUpdate)
            {
                var booking = ExistedBookings.Where(x => x.Id != id).ToList();
                return booking.Count > 0;
            }

            return ExistedBookings.Count > 0;
        }

        private void TestRabbit()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "hello",
                         durable: false,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);

            string message = "Hello World!";
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: "hello",
                                 basicProperties: null,
                                 body: body);
        }

    }
}
