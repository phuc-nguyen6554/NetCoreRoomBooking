using LeaveRequest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.Models
{
    public class LeaveType
    {
        public int Id { get; set; }
 
        public string Name { get; set; }

        public List<Request> Requests { get; set; }

    }
}
