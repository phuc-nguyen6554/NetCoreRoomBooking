using Microsoft.Extensions.DependencyInjection;
using RoomBookingService.Services;
using RoomBookingService.Services.Implements;
using MailService.MailServiceHttp;

namespace RoomBookingService.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureService(this IServiceCollection service)
        {
            service.AddScoped<IBookingService, BookingService>();
            service.AddScoped<IRoomService, RoomService>();
            service.AddScoped<IMailHttp, MailHttp>();
        }
    }
}
