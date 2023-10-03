using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Domain.Enums;
using Domain.Models;
using Domain.Models.DTOs;
using Domain.Validations;
using FluentValidation;
using Infrastructure.ExtensionMethods;
using Infrastructure.Mapper;

namespace Infrastructure.Implementations.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<DisplaySimpleUserDTO?> CreateUserAsync(RegisterUserDTO userToCreate, CancellationToken ct)
        {
            this.ValidateUser(userToCreate);
            DomainUser? repeatedUser = await FindUserByEmailAsync(userToCreate.Email!, ct);

            if (repeatedUser is not null)
                throw new InvalidOperationException();
            
            DomainUser user = userToCreate.MapToDomainUser();
            user.Id = Guid.NewGuid();
            user.FullName = $"{userToCreate.FirstName} {userToCreate.LastName}";
            user.Role = UserRoles.EMPLOYEE;
            user.Password = user.Password!.GetSha256();
            user.IsActive = true;
            user.CreatedAt = DateTime.Now;
            user = await _userRepository.CreateUserAsync(user, ct);
            return user.MapToDisplaySimpleUserDTO();
        }

        public async Task<DomainUser?> FindUserByEmailAsync(string email, CancellationToken ct)
        {
            return await _userRepository.ReadUserByEmailAsync(email, ct);
        }

        private void ValidateUser(RegisterUserDTO user)
        {
            var validator = new InsertUserDTOValidator();
            validator.ValidateAndThrow(user);
        }
    }
}
