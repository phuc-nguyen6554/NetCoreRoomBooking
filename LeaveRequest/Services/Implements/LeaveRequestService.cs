using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LeaveRequest.DTO.Leave;
using LeaveRequest.Models;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Cache;

namespace LeaveRequest.Services.Implements
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly LeaveRequestServiceContext _context;
        private readonly IMapper _mapper;
        private readonly IScopedCache _cache;

        public LeaveRequestService(LeaveRequestServiceContext context, IMapper mapper, IScopedCache cache)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
        }

        //get leave request
        public async Task<List<LeaveRequestResponse>> GetLeaveRequestAsync(LeaveRequestListRequest b)
        {
            var leave = new List<Request>();
     
            if (b.LeaveDates != null)
            {
                leave = await _context.LeaveRequests.Include(b => b.LeaveTypes).Where(x => x.LeaveDates == b.LeaveDates).ToListAsync();
            }
            
            if (b.LeaveTime != null)
            {
                leave = await _context.LeaveRequests.Include(b => b.LeaveTypes).Where(x => x.LeaveTime == b.LeaveTime).ToListAsync();
            }
            
            if (b.LeaveTypeId != 0)
            {
                leave = await _context.LeaveRequests.Include(x => x.LeaveTypes).Where(x => x.LeaveTypeId == b.LeaveTypeId).ToListAsync();
            }
            else if((b.LeaveTime == null) && (b.LeaveTypeId == 0) && (b.LeaveDates == null))
            {
                leave = await _context.LeaveRequests.Include(b => b.LeaveTypes).ToListAsync();
            }
            
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
            foreach(var date in request.LeaveDates)
            {
                var entity = new Request
                {
                    LeaveDates = date,
                    LeaveTime = request.LeaveTime,
                    LeaveTypeId = request.LeaveTypeId,
                    Name = request.Name,
                    Reason = request.Reason,
                    MemberEmail = _cache.Email,
                    MemberName = _cache.Username
                };
                _context.LeaveRequests.Add(entity);
            }

            await _context.SaveChangesAsync();

            return _mapper.Map<LeaveRequestCreateResponse>(new LeaveRequestCreateResponse());
        }

        public Task<List<LeaveRequestResponse>> GetLeaveRequestAsync(LeaveRequestCreateRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
