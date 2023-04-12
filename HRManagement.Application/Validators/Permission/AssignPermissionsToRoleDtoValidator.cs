using FluentValidation;
using HRManagement.Application.Dtos.Permissions;

namespace HRManagement.Application.Validators.Permission
{
    public class AssignPermissionsToRoleDtoValidator : AbstractValidator<AssignPermissionsToRoleDto>
    {
        public AssignPermissionsToRoleDtoValidator()
        {
            RuleFor(r => r.RoleId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(r => r.PermissionIds)
                .Must(r => r.Count > 0)
                .WithMessage("At least one permission is required.");

        }
    }
}
