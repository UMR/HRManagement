using FluentValidation;
using HRManagement.Application.Dtos.Users;

namespace HRManagement.Application.Features.Users.Validators
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(c => c.FirstName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(128).WithMessage("{PropertyName} must not exceed 128 characters");

            RuleFor(c => c.LastName)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull()
               .MaximumLength(1024).WithMessage("{PropertyName} must not exceed 128 characters");

            RuleFor(c => c.Email)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull()
              .MaximumLength(1024).WithMessage("{PropertyName} must not exceed 256 characters");

            RuleFor(c => c.Password)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull()
              .MaximumLength(1024).WithMessage("{PropertyName} must not exceed 128 characters");
        }
    }
}
