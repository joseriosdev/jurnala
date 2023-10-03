using Domain.Enums;
using Domain.Models;
using Domain.Models.DTOs;
using Infrastructure.Models;
using System.Diagnostics;

namespace Infrastructure.Mapper
{
    public static class ExtensionMethodMapper
    {
        public static User MapToUser(this InsertUserDTO dto)
        {
            var user = new User();
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Email = dto.Email;
            user.Password = dto.Password;
            return user;
        }

        public static User MapToUser(this DomainUser u)
        {
            var user = new User();
            user.Id = u.Id.ToString();
            user.ManagerId = u.ManagerId?.ToString();
            user.FirstName = u.FirstName;
            user.LastName = u.LastName;
            user.FullName = u.FullName;
            user.Email = u.Email;
            user.Role = u.Role.ToString();
            user.Password = u.Password;
            user.IsActive = u.IsActive;
            user.CreatedAt = u.CreatedAt;
            user.UpdatedAt = u.UpdatedAt;
            user.UpdatedBy = u.UpdatedBy?.ToString();
            user.CreatedBy = u.CreatedBy?.ToString();
            return user;
        }

        public static DomainUser MapToDomainUser(this User u)
        {
            var user = new DomainUser();
            user.Id = Guid.Parse(u.Id);
            user.ManagerId = u.ManagerId is null ? null : Guid.Parse(u.ManagerId);
            user.FirstName = u.FirstName;
            user.LastName = u.LastName;
            user.FullName = u.FullName;
            user.Email = u.Email;
            user.Role = (UserRoles)Enum.Parse(typeof(UserRoles), u.Role!);
            user.Password = u.Password;
            user.IsActive = u.IsActive;
            user.CreatedAt = u.CreatedAt;
            user.UpdatedAt = u.UpdatedAt;
            user.UpdatedBy = u.UpdatedBy is null ? null : Guid.Parse(u.UpdatedBy);
            user.CreatedBy = u.CreatedBy is null ? null : Guid.Parse(u.CreatedBy);
            return user;
        }

        public static DomainUser MapToDomainUser(this InsertUserDTO dto)
        {
            var user = new DomainUser();
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Email = dto.Email;
            user.Password = dto.Password;
            return user;
        }

        public static DisplaySimpleUserDTO MapToDisplaySimpleUserDTO(this DomainUser u)
        {
            return new DisplaySimpleUserDTO(
                u.Id.ToString(),
                u.FullName,
                u.Email,
                u.Role.ToString(),
                u.CreatedAt);
        }
    }
}
