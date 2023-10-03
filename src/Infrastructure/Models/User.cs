using System;
using System.Collections.Generic;

namespace Infrastructure.Models
{
    public partial class User
    {
        public User()
        {
            AssignedEmployeeEmployees = new HashSet<AssignedEmployee>();
            AssignedEmployeeManagers = new HashSet<AssignedEmployee>();
            AssignedProjects = new HashSet<AssignedProject>();
            InverseCreatedByNavigation = new HashSet<User>();
            InverseManager = new HashSet<User>();
            InverseUpdatedByNavigation = new HashSet<User>();
            ProjectCreatedByNavigations = new HashSet<Project>();
            ProjectUpdatedByNavigations = new HashSet<Project>();
            TimesheetCreatedByNavigations = new HashSet<Timesheet>();
            TimesheetUpdatedByNavigations = new HashSet<Timesheet>();
            TimeslotCreatedByNavigations = new HashSet<Timeslot>();
            TimeslotUpdatedByNavigations = new HashSet<Timeslot>();
        }

        public string Id { get; set; } = null!;
        public string? ManagerId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public string? Password { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public string? CreatedBy { get; set; }

        public virtual User? CreatedByNavigation { get; set; }
        public virtual User? Manager { get; set; }
        public virtual User? UpdatedByNavigation { get; set; }
        public virtual ICollection<AssignedEmployee> AssignedEmployeeEmployees { get; set; }
        public virtual ICollection<AssignedEmployee> AssignedEmployeeManagers { get; set; }
        public virtual ICollection<AssignedProject> AssignedProjects { get; set; }
        public virtual ICollection<User> InverseCreatedByNavigation { get; set; }
        public virtual ICollection<User> InverseManager { get; set; }
        public virtual ICollection<User> InverseUpdatedByNavigation { get; set; }
        public virtual ICollection<Project> ProjectCreatedByNavigations { get; set; }
        public virtual ICollection<Project> ProjectUpdatedByNavigations { get; set; }
        public virtual ICollection<Timesheet> TimesheetCreatedByNavigations { get; set; }
        public virtual ICollection<Timesheet> TimesheetUpdatedByNavigations { get; set; }
        public virtual ICollection<Timeslot> TimeslotCreatedByNavigations { get; set; }
        public virtual ICollection<Timeslot> TimeslotUpdatedByNavigations { get; set; }
    }
}
