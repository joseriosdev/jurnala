using System;
using System.Collections.Generic;

namespace Infrastructure.Models
{
    public partial class Project
    {
        public Project()
        {
            AssignedProjects = new HashSet<AssignedProject>();
            InverseParentProject = new HashSet<Project>();
        }

        public string Id { get; set; } = null!;
        public string? ParentProjectId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int ProjectCode { get; set; }
        public bool IsActive { get; set; }
        public int? CurrentDeep { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public string? CreatedBy { get; set; }

        public virtual User? CreatedByNavigation { get; set; }
        public virtual Project? ParentProject { get; set; }
        public virtual User? UpdatedByNavigation { get; set; }
        public virtual ICollection<AssignedProject> AssignedProjects { get; set; }
        public virtual ICollection<Project> InverseParentProject { get; set; }
    }
}
