using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class DomainTimesheet
    {
        public DomainTimesheet()
        {
            Timeslots = new HashSet<DomainTimeslot>();
        }

        public string Id { get; set; } = null!;
        public string? Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public string? CreatedBy { get; set; }

        public virtual DomainUser? CreatedByNavigation { get; set; }
        public virtual DomainUser? UpdatedByNavigation { get; set; }
        public virtual ICollection<DomainTimeslot> Timeslots { get; set; }
    }
}
