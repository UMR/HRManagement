using FluentValidation;
using HRManagement.Application.Dtos.Users;

namespace HRManagement.Application.Validators.Users
{
    public class CreateUserDtoValidator : AbstractValidator<UserForCreateDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(u => u.FirstName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(128).WithMessage("{PropertyName} must not exceed 128 characters");

            RuleFor(u => u.LastName)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull()
               .MaximumLength(1024).WithMessage("{PropertyName} must not exceed 128 characters");

            RuleFor(u => u.Email)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull()
              .MaximumLength(1024).WithMessage("{PropertyName} must not exceed 256 characters");

            RuleFor(u => u.Password)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull()
              .MaximumLength(1024).WithMessage("{PropertyName} must not exceed 64 characters");
        }
    }
}
