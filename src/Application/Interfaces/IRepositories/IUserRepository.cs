
using Domain.Models;

namespace Application.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        Task<DomainUser> CreateUserAsync(DomainUser userToCreate, CancellationToken ct);
        Task<DomainUser?> ReadUserByEmailAsync(string email, CancellationToken ct);
        Task<DomainUser> UpdateUserAsync(DomainUser userToUpdate, CancellationToken ct);
        Task SoftDeleteUserAsync(string email, CancellationToken ct);
    }
}
