using Application.Interfaces.IRepositories;
using Domain.Constants;
using Domain.Models;
using Infrastructure.Mapper;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Utils;

namespace Infrastructure.Implementations.Repositiories
{
    public class UserRepository : IUserRepository
    {
        private readonly JurnalaContext _context;
        public UserRepository(JurnalaContext context)
        {
            _context = context;
        }
        

        public async Task<DomainUser> CreateUserAsync(DomainUser userToCreate, CancellationToken ct)
        {
            User? userToInsert = userToCreate.MapToUser();
            await _context.Users.AddAsync(userToInsert, ct);
            await _context.SaveChangesAsync(ct);
            return userToInsert.MapToDomainUser();
        }

        public async Task<DomainUser?> ReadUserByEmailAsync(string email, CancellationToken ct)
        {
            User? userFound = await _context.Users.FirstOrDefaultAsync(u => u.Email == email, ct);
            DomainUser? userToReturn = userFound?.MapToDomainUser();
            return userToReturn;
        }

        public async Task<DomainUser> UpdateUserAsync(DomainUser userToUpdate, CancellationToken ct)
        {
            User? userToUpdateInDB = await _context.Users.FindAsync(userToUpdate.Id.ToString(), ct);
            if (userToUpdateInDB is null) throw new KeyNotFoundException(JurnalaErrorMessage.USER_NOT_FOUND);
            ObjectMapping.UpdatePropertiesWithMatchingNames(userToUpdateInDB, userToUpdate);
            _context.Entry(userToUpdateInDB).State = EntityState.Modified;
            await _context.SaveChangesAsync(ct);
            return userToUpdateInDB.MapToDomainUser();
        }

        public async Task SoftDeleteUserAsync(string email, CancellationToken ct)
        {
            User? userFound = await _context.Users.FirstOrDefaultAsync(u => u.Email == email, ct);
            if (userFound is not null)
            {
                if (userFound.IsActive)
                {
                    userFound.IsActive = false;
                    userFound.Email += "_DELETED";
                    _context.Entry(userFound).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                else
                    throw new InvalidOperationException(JurnalaErrorMessage.USER_ALREADY_EXITS);
            }
            else
                throw new KeyNotFoundException(JurnalaErrorMessage.USER_NOT_FOUND);
        }

        private User UpdateUserFromDomainUser(User userToUpdate, DomainUser userUpdater)
        {
            return null;
        }
    }
}
