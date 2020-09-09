using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.Models
{
    public class Request
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime LeaveDates { get; set; }

        public String LeaveTime { get; set; }

        public int LeaveTypeId { get; set; }

        public String Reason { get; set; }

        public String MemberName { get; set; }

        public String MemberEmail { get; set; }

    }
}
