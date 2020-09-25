using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LeaveRequest.DTO.Leave;
using Shared.Exceptions;
using LeaveRequest.Services;
using LeaveRequest.Models;

namespace LeaveRequestController.Controllers
{
    [Route("request")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {
        private readonly ILeaveRequestService _leaveService;

        public LeaveRequestController(ILeaveRequestService leaveService)
        {
            _leaveService = leaveService;
        }


        [HttpGet("list")]
        [ProducesResponseType(typeof(List<LeaveRequestResponse>), 200)]
        public async Task<IActionResult> GetLeaveRequestsAsync([FromQuery]LeaveRequestListRequest request)
        {
            return Ok(await _leaveService.GetLeaveRequestAsync(request));
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("create")]
        [ProducesResponseType(typeof(LeaveRequestCreateResponse), 200)]
        public async Task<IActionResult> CreateLeaveAsync([FromBody] LeaveRequestCreateRequest request)
        {
            if (!ModelState.IsValid)
                throw new ServiceException(400, "Model is invalid");

            request.MemberName = Request.Headers["X-Forwarded-Username"];
            request.MemberEmail = Request.Headers["X-Forwarded-Email"];

            return Ok(await _leaveService.CreateLeaveRequestAsync(request));
        }

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteLeaveAsync(int id)
        {
            await _leaveService.DeleteLeaveRequestAsync(id);
            return Ok();
        }
    }
}
