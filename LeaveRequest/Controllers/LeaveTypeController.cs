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
    [Route("/leave-types")]
    [ApiController]
    public class LeaveTypeController : ControllerBase
    {
        private readonly LeaveRequestServiceContext _context;

        public LeaveTypeController(LeaveRequestServiceContext context)
        {
            _context = context;
        }

        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<LeaveType>>> GetLeaveTypes()
        {
            return await _context.LeaveTypes.ToListAsync();
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveType>> GetLeaveType(int id)
        {
            var lType = await _context.LeaveTypes.FindAsync(id);

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
                if (!LeaveTypeExists(id))
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
            _context.LeaveTypes.Add(leavType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLeaveType", new { id = leavType.Id }, leavType);
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LeaveType>> DeleteRoom(int id)
        {
            var leaveTypes = await _context.LeaveTypes.FindAsync(id);
            if (leaveTypes == null)
            {
                return NotFound();
            }

            _context.LeaveTypes.Remove(leaveTypes);
            await _context.SaveChangesAsync();

            return leaveTypes;
        }

        [Authorize]
        [HttpGet("Test")]
        public ActionResult Test()
        {
            return Ok(new { email = User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault()?.Value});
        }

        private bool LeaveTypeExists(int id)
        {
            return _context.LeaveTypes.Any(e => e.Id == id);
        }
    }
}
