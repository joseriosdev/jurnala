namespace Domain.Models.DTOs
{
    public class UpdateUserDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ManagerId { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
