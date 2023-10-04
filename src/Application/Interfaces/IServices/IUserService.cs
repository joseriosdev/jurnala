
using Domain.Models.DTOs;
using Domain.Models;

namespace Application.Interfaces.IServices
{
    public interface IUserService
    {
        Task<DisplaySimpleUserDTO?> InsertUserAsync(RegisterUserDTO userToCreate, CancellationToken ct);
        Task<DomainUser?> FindUserByEmailAsync(string email, CancellationToken ct);
        Task<string> RemoveUserAsync(string email, CancellationToken ct);
    }
}
