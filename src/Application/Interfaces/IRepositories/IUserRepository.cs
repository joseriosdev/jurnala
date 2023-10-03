
using Domain.Models;
using Domain.Models.DTOs;

namespace Application.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        Task<DomainUser> CreateUserAsync(DomainUser userToCreate, CancellationToken ct);
        Task<DomainUser?> ReadUserByEmailAsync(string email, CancellationToken ct);
    }
}
