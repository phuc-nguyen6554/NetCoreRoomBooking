using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LeaveRequest.Models;

namespace LeaveRequest.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveTypeController : ControllerBase
    {
        private readonly LeaveRequestServiceContext _context;

        public LeaveTypeController(LeaveRequestServiceContext context)
        {
            _context = context;
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeaveType>>> GetleaveTypes()
        {
            return await _context.leaveTypes.ToListAsync();
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveType>> GetLeaveType(int id)
        {
            var lType = await _context.leaveTypes.FindAsync(id);

            if (lType == null)
            {
                return NotFound();
            }

            return lType;
        }

        // PUT: api/Rooms/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, LeaveType lType)
        {
            if (id != lType.Id)
            {
                return BadRequest();
            }

            _context.Entry(lType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Rooms
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<LeaveType>> PostRoom(LeaveType leavType)
        {
            _context.leaveTypes.Add(leavType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLeaveType", new { id = leavType.Id }, leavType);
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LeaveType>> DeleteRoom(int id)
        {
            var leaveTypes = await _context.leaveTypes.FindAsync(id);
            if (leaveTypes == null)
            {
                return NotFound();
            }

            _context.leaveTypes.Remove(leaveTypes);
            await _context.SaveChangesAsync();

            return leaveTypes;
        }

        [Authorize]
        [HttpGet("Test")]
        public ActionResult Test()
        {
            return Ok(new { email = User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault()?.Value});
        }

        private bool RoomExists(int id)
        {
            return _context.leaveTypes.Any(e => e.Id == id);
        }
    }
}
