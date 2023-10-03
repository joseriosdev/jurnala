using System;
using System.Collections.Generic;

namespace Infrastructure.Models
{
    public partial class Timesheet
    {
        public Timesheet()
        {
            Timeslots = new HashSet<Timeslot>();
        }

        public string Id { get; set; } = null!;
        public string? Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public string? CreatedBy { get; set; }

        public virtual User? CreatedByNavigation { get; set; }
        public virtual User? UpdatedByNavigation { get; set; }
        public virtual ICollection<Timeslot> Timeslots { get; set; }
    }
}
