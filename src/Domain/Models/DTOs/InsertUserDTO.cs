using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.DTOs
{
    public record InsertUserDTO
    (
        string? FirstName,
        string? LastName,
        string? Email,
        string? Password
    );
}
