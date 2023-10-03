namespace Domain.Models.DTOs
{
    public record RegisterUserDTO
    (
        string? FirstName,
        string? LastName,
        string? Email,
        string? Password
    );
}
