using System;
using System.Collections.Generic;

namespace Infrastructure.Models
{
    public partial class Timeslot
    {
        public string Id { get; set; } = null!;
        public float? HoursWorked { get; set; }
        public string? Description { get; set; }
        public string? ProjectId { get; set; }
        public string? TimesheetId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public string? CreatedBy { get; set; }

        public virtual User? CreatedByNavigation { get; set; }
        public virtual Timesheet? Timesheet { get; set; }
        public virtual User? UpdatedByNavigation { get; set; }
    }
}
