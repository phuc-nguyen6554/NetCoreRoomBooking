using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoomBookingService.DTO.Rooms;
using RoomBookingService.Models;
using RoomBookingService.Models.Rooms;
using RoomBookingService.Services;

namespace RoomBookingService.Controllers
{
    [Route("rooms")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _service;

        public RoomsController(IRoomService service)
        {
            _service = service;
        }

        // GET: api/Rooms
        [HttpGet]
        [ProducesResponseType(typeof(List<RoomListResponse>), 200)]
        public async Task<ActionResult<ICollection<RoomListResponse>>> Getrooms()
        {
            var header = Request.Headers;
            var name = Request.Headers["X-Forwarded-Name"];
            return await _service.GetRoomListAsync();
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDetailResponse>> GetRoom(int id)
        {
            return await _service.GetRoomDetailAsync(id);
        }

        // PUT: api/Rooms/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<RoomUpdateResponse>> PutRoom(int id, RoomUpdateRequest room)
        {
            return await _service.UpdateRoomAsync(id, room);
        }

        // POST: api/Rooms
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<RoomCreateResponse>> PostRoom(RoomCreateRequest room)
        {
            return await _service.CreateRoomAsync(room);
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRoom(int id)
        {
            await _service.DeleteRoomAsync(id);
            return Ok();
        }
    }
}
