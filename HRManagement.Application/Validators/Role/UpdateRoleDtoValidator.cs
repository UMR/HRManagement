using FluentValidation;
using HRManagement.Application.Dtos.Roles;

namespace HRManagement.Application.Validators.Role
{
    public class UpdateRoleDtoValidator : AbstractValidator<RoleForUpdateDto>
    {
        public UpdateRoleDtoValidator()
        {
            RuleFor(r => r.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(r => r.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(128).WithMessage("{PropertyName} must not exceed 128 characters");

        }
    }
}
