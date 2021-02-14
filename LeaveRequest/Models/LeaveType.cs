using LeaveRequest.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.Models
{
    [Table("LR_LeaveTypes")]
    public class LeaveType
    {
        public int Id { get; set; }
 
        public string Name { get; set; }

        public List<Request> Requests { get; set; }

    }
}
