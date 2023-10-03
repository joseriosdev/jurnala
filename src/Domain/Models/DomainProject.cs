using Domain.Models.Commons;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class DomainProject : IEntity, IAuditableEntity
    {
        public DomainProject()
        {
            InverseParentProjectNavigation = new HashSet<DomainProject>();
        }

        public Guid Id { get; set; }
        public string? ParentProject { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int ProjectCode { get; set; }
        public bool IsActive { get; set; }
        public int? CurrentDeep { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public Guid? CreatedBy { get; set; }

        public virtual DomainUser? CreatedByNavigation { get; set; }
        public virtual DomainProject? ParentProjectNavigation { get; set; }
        public virtual DomainUser? UpdatedByNavigation { get; set; }
        public virtual ICollection<DomainProject> InverseParentProjectNavigation { get; set; }
    }
}
