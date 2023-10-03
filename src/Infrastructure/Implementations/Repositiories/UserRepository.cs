using Application.Interfaces.IRepositories;
using Domain.Models;
using Infrastructure.Mapper;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
