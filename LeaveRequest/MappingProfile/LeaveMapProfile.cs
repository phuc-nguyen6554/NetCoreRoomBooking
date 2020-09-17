using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeaveRequest.Models;
using LeaveRequest.DTO.Leave;

namespace LeaveRequest.MappingProfile
{
    public class LeaveMapProfile : Profile
    {
        public LeaveMapProfile()
        {
            CreateMap<Request, LeaveRequestResponse>().ForMember(x=> x.LeaveType, opt => opt.MapFrom(x => x.LeaveTypes.Name));

            CreateMap<Request, LeaveRequestCreateResponse>();
            CreateMap<LeaveRequestCreateRequest, Request>();
        }
    }
}
