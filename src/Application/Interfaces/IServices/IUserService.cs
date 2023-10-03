
using Domain.Models.DTOs;
using Domain.Models;

namespace Application.Interfaces.IServices
{
    public interface IUserService
    {
        Task<DisplaySimpleUserDTO?> CreateUserAsync(RegisterUserDTO userToCreate, CancellationToken ct);
        Task<DomainUser?> FindUserByEmailAsync(string email, CancellationToken ct);
    }
}
