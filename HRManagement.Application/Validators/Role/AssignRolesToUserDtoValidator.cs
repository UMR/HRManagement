using FluentValidation;
using HRManagement.Application.Dtos.Roles;

namespace HRManagement.Application.Validators.Role
{
    public class AssignRolesToUserDtoValidator : AbstractValidator<AssignRolesToUserDto>
    {
        public AssignRolesToUserDtoValidator()
        {
            RuleFor(r => r.UserId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(r => r.RoleIds)
                .Must(r => r.Count > 0)
                .WithMessage("At least one role is required.");

        }
    }
}
