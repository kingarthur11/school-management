using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Responses
{
    public class StaffResponse
    {
        public Guid StaffId { get; set; }
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;
        public string MiddleName { get; set; }
        public string PhoneNumber { get; set; }
        public Guid? JobTitleId { get; set; }
        public Guid? DepartmentId { get; set; }
    }
}
