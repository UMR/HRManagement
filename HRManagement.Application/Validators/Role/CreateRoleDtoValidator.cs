using FluentValidation;
using HRManagement.Application.Dtos.Roles;

namespace HRManagement.Application.Validators.Users
{
    public class CreateRoleDtoValidator : AbstractValidator<RoleForCreateDto>
    {
        public CreateRoleDtoValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(128).WithMessage("{PropertyName} must not exceed 128 characters");
            
        }
    }
}
