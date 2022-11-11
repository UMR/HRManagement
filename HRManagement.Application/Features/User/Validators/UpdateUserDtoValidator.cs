using FluentValidation;
using HRManagement.Application.Dtos.User;

namespace HRManagement.Application.Features.User.Validators
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(c => c.UserId)
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
