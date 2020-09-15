using Microsoft.Extensions.DependencyInjection;
using RoomBookingService.Services;
using RoomBookingService.Services.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomBookingService.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureService(this IServiceCollection service)
        {
            service.AddScoped<IBookingService, BookingService>();
            service.AddScoped<IRoomService, RoomService>();
        }
    }
}
