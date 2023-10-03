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
