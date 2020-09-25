using LeaveRequest.DTO.Leave;
using LeaveRequest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.Services
{
    public interface ILeaveRequestService
    {
        Task<List<LeaveRequestResponse>> GetLeaveRequestAsync(LeaveRequestListRequest request);

        Task<LeaveRequestCreateResponse> CreateLeaveRequestAsync(LeaveRequestCreateRequest request);

        Task DeleteLeaveRequestAsync(int id);
    }
}
