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
using Shared.Data;
using Shared.Exceptions;

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
        public async Task<List<LeaveRequestResponse>> GetLeaveRequestAsync(LeaveRequestListRequest request)
        {
            var leave = new List<Request>();
     
            if (request.LeaveDates != null)
            {
                //admin can list all leave request
                if (_cache.Role == Constrain.AdminRole)
                {
                    leave = await _context.LeaveRequests.Include(b => b.LeaveTypes).Where(x => x.LeaveDates.Date == request.LeaveDates.Value.Date).ToListAsync();
                }
                //user can list of his/here leave request
                else
                { 
                    leave = await _context.LeaveRequests.Include(b => b.LeaveTypes).Where(l => l.MemberEmail == _cache.Email).Where(x => x.LeaveDates.Date == request.LeaveDates.Value.Date).ToListAsync();
                }
            }
            
            if (request.LeaveTime != null)
            {
                leave = await _context.LeaveRequests.Include(b => b.LeaveTypes).Where(x => x.LeaveTime == request.LeaveTime).ToListAsync();
            }
            
            if (request.LeaveTypeId != 0)
            {
                leave = await _context.LeaveRequests.Include(x => x.LeaveTypes).Where(x => x.LeaveTypeId == request.LeaveTypeId).ToListAsync();
            }
            //get all leave type
            if (request.LeaveTypeId == -1)
            {
                leave = await _context.LeaveRequests.Include(x => x.LeaveTypes).ToListAsync();
            }
            else if((request.LeaveTime == null) && (request.LeaveTypeId == 0) && (request.LeaveDates == null))
            {
                //admin can list all leave request
                if (_cache.Role == Constrain.AdminRole)
                {
                    leave = await _context.LeaveRequests.Include(b => b.LeaveTypes).ToListAsync();
                }
                //user can list of his/here leave request
                else
                {
                    leave = await _context.LeaveRequests.Where(l => l.MemberEmail == _cache.Email).Include(b => b.LeaveTypes).ToListAsync();
                }
            }
            
            return _mapper.Map<List<LeaveRequestResponse>>(leave);
        }

        //delete leave request
        public async Task DeleteLeaveRequestAsync(int id)
        {
            var leave = await _context.LeaveRequests.FindAsync(id);
            
            if (leave.MemberEmail != _cache.Email && _cache.Role != Constrain.AdminRole)
            {
                throw new ServiceException(401, "You don't have permission to delete this Leave Request");
            }

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
