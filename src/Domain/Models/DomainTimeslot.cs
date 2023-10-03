namespace Domain.Models
{
    public partial class DomainTimeslot
    {
        public string Id { get; set; } = null!;
        public double? HoursWorked { get; set; }
        public string? Description { get; set; }
        public string? ProjectId { get; set; }
        public string? TimesheetId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public string? CreatedBy { get; set; }

        public virtual DomainUser? CreatedByNavigation { get; set; }
        public virtual DomainTimesheet? Timesheet { get; set; }
        public virtual DomainUser? UpdatedByNavigation { get; set; }
    }
}
