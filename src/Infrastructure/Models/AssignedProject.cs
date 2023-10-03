using System;
using System.Collections.Generic;

namespace Infrastructure.Models
{
    public partial class AssignedProject
    {
        public string Id { get; set; } = null!;
        public string? UserId { get; set; }
        public string? ProjectId { get; set; }

        public virtual Project? Project { get; set; }
        public virtual User? User { get; set; }
    }
}
