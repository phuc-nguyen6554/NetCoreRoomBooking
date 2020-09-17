using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LeaveRequest.DTO.Leave;
using LeaveRequest.Models;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Exceptions;

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
            var leave = await _context.LeaveRequests.FindAsync(id);

            _context.LeaveRequests.Remove(leave);
            await _context.SaveChangesAsync();
        }

        //create leave request
        public async Task<LeaveRequestCreateResponse> CreateLeaveRequestAsync(LeaveRequestCreateRequest request)
        {
            var leave = _mapper.Map<Request>(request);

            _context.LeaveRequests.Add(leave);
            await _context.SaveChangesAsync();

            return _mapper.Map<LeaveRequestCreateResponse>(leave);
        }
    }
}
