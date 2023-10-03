using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.DTOs
{
    public record DisplaySimpleUserDTO
    (
        string? Id,
        string? FullName,
        string? Email,
        string? Role,
        DateTime? CreatedAt
    );
}
