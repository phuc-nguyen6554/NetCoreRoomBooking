using LeaveRequest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveRequest.DTO.Leave
{
    public class LeaveRequestCreateRequest
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime LeaveDates { get; set; }
        public string LeaveTime { get; set; }
        public LeaveType LeaveTypes { get; set; }
        public int LeaveTypeId { get; set; }
        public string Reason { get; set; }
        public string MemberName { get; set; }
        public string MemberEmail { get; set; }
    }
}
