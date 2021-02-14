using System;

namespace LeaveRequest.DTO.Leave
{
    public class LeaveRequestResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime LeaveDates { get; set; }
        public string LeaveTime { get; set; }
        public string LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public string Reason { get; set; }
        public string MemberName { get; set; }
        public string MemberEmail { get; set; }
    }
}
