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

namespace LeaveRequestController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {
        private readonly LeaveRequestServiceContext _context;

        public LeaveRequestController(LeaveRequestServiceContext context)
        {
            _context = context;
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetLeaveRequests()
        {
            // Get Upcomming Booking event
            return await _context.leaveRequests.Include(b => b.leaveTypes).ToListAsync();
        }

        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetLeaveRequest(int id)
        {
            var reQuest = await _context.leaveRequests.FindAsync(id);

            if (reQuest == null)
            {
                return NotFound();
            }

            return reQuest;
        }

        // POST: api/Bookings
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Request>> PostLeaveQuest(Request leaveRequests)
        {

            // Get info from JWT
            //string username = User.Claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault()?.Value;
            //string email = User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault()?.Value;

            string username = Request.Headers["X-Forwarded-Username"];
            string email = Request.Headers["X-Forwarded-Email"];

            leaveRequests.MemberName = username;
            leaveRequests.MemberEmail = email;

            _context.leaveRequests.Add(leaveRequests);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLeaveRequest", new { id = leaveRequests.Id }, leaveRequests);
        }

        // DELETE: api/LeaveRequest/1
        [HttpDelete("{id}")]
        public async Task<ActionResult<Request>> DeleteLeaveRequest(int id)
        {
            var leave = await _context.leaveRequests.FindAsync(id);
            if (leave == null)
            {
                return NotFound();
            }

            //string email = User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault()?.Value;
            string email = Request.Headers["X-Forwarded-Email"];

            if (leave.MemberEmail != email)
            {
                return Unauthorized("You don't have permission to delete this booking");
            }

            _context.leaveRequests.Remove(leave);
            await _context.SaveChangesAsync();

            return leave;
        }

        private bool BookingExists(int id)
        {
            return _context.leaveRequests.Any(e => e.Id == id);
        }
    }
}
