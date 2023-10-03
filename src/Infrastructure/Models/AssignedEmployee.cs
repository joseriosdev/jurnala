using System;
using System.Collections.Generic;

namespace Infrastructure.Models
{
    public partial class AssignedEmployee
    {
        public string Id { get; set; } = null!;
        public string? EmployeeId { get; set; }
        public string? ManagerId { get; set; }

        public virtual User? Employee { get; set; }
        public virtual User? Manager { get; set; }
    }
}
