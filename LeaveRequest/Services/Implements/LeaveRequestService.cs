using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LeaveRequest.DTO.Leave;
using LeaveRequest.Models;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace LeaveRequest.Services.Implements
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly LeaveRequestServiceContext _context;
        private readonly IMapper _mapper;

        public LeaveRequestService(LeaveRequestServiceContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //get leave request
        public async Task<List<LeaveRequestResponse>> GetLeaveRequestAsync()
        {
            var leave = await _context.LeaveRequests.Include(b => b.LeaveTypes).ToListAsync();

            return _mapper.Map<List<LeaveRequestResponse>>(leave);
        }

        //delete leave request
        public async Task DeleteLeaveRequestAsync(int id)
        {
            throw new NotImplementedException();
        }

        //create leave request
        public async Task<LeaveRequestCreateResponse> CreateLeaveRequestAsync(LeaveRequestCreateResponse request)
        {
            var booking = await _context.LeaveRequests
                .Include(inc => inc.LeaveTypeId)
                .FirstOrDefaultAsync();

            var validate = false;

            if (!validate)
                throw new ServiceException(400, "Model invalid");

            var isExists = false;
            if (!isExists)
                throw new ServiceException(404, "Model not found");

            throw new NotImplementedException();
        }

        public Task<LeaveRequestCreateResponse> CreateLeaveRequestAsync(LeaveRequestCreateRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
