
using Domain.Models.DTOs;
using Domain.Models;

namespace Application.Interfaces.IServices
{
    public interface IUserService
    {
        Task<DisplaySimpleUserDTO?> CreateUserAsync(InsertUserDTO userToCreate, CancellationToken ct);
        Task<DomainUser?> FindUserByEmailAsync(string email, CancellationToken ct);
    }
}
