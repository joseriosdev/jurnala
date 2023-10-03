using Domain.Enums;
using Domain.Models.Commons;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class DomainUser : IEntity, IAuditableEntity
    {
        public Guid Id { get; set; }
        public Guid? ManagerId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public UserRoles Role { get; set; }
        public string? Password { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public Guid? CreatedBy { get; set; }
    }
}
