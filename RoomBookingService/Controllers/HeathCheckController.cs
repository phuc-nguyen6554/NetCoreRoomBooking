using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RoomBookingService.Controllers
{
    [Route("heathcheck")]
    [ApiController]
    public class HeathCheckController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Check()
        {
            return Ok("Ok");
        }
    }
}
