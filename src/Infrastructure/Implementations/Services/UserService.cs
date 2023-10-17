using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Domain.Constants;
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

        public async Task<DisplaySimpleUserDTO?> InsertUserAsync(RegisterUserDTO userToCreate, CancellationToken ct)
        {
            this.ValidateUser(userToCreate);
            DomainUser? repeatedUser = await FindUserByEmailAsync(userToCreate.Email!, ct);

            if (repeatedUser is not null)
                throw new InvalidOperationException(JurnalaErrorMessage.USER_ALREADY_EXITS);
            
            DomainUser user = userToCreate.MapToDomainUser();
            user.Id = Guid.NewGuid();
            user.FullName = $"{userToCreate.FirstName} {userToCreate.LastName}";
            user.Role = UserRoles.EMPLOYEE;
            user.Password = user.Password!.GetSha256();
            user.IsActive = true;
            user.CreatedAt = DateTime.UtcNow;
            user = await _userRepository.CreateUserAsync(user, ct);
            return user.MapToDisplaySimpleUserDTO();
        }

        public async Task<DomainUser?> FindUserByEmailAsync(string email, CancellationToken ct)
        {
            return await _userRepository.ReadUserByEmailAsync(email, ct);
        }

        public async Task<string> RemoveUserAsync(string email, CancellationToken ct)
        {
            if (!email.IsValidEmailAddress())
                throw new ArgumentException(JurnalaErrorMessage.TEXT_WRONG_EMAIL, nameof(email));

            await _userRepository.SoftDeleteUserAsync(email, ct);
            return $"User {email} was deleted at {DateTime.UtcNow}";
        }

        public Task<UpdateUserDTO?> EditUserAsync(string email, UpdateUserDTO userToUpdate, CancellationToken ct)
        {
            throw new NotImplementedException();
        }


        // ********** PRIVATE METHODS **********

        private void ValidateUser(RegisterUserDTO user)
        {
            var validator = new InsertUserDTOValidator();
            validator.ValidateAndThrow(user);
        }

        
    }
}
