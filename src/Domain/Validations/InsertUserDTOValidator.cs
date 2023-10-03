using Domain.Models.DTOs;
using FluentValidation;

namespace Domain.Validations
{
    public class InsertUserDTOValidator : AbstractValidator<InsertUserDTO>
    {
        public InsertUserDTOValidator()
        {
            RuleFor(dto => dto.FirstName)
                .NotEmpty().WithMessage("First Name is required")
                .MinimumLength(2).MaximumLength(20); ;
            RuleFor(dto => dto.LastName)
                .NotEmpty().WithMessage("Last Name is required")
                .MinimumLength(2).MaximumLength(20); ;
            RuleFor(dto => dto.Email)
                .NotEmpty().EmailAddress();
            RuleFor(dto => dto.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters.");
        }
    }
}
