using FluentValidation;
using HRManagement.Application.Dtos.Permissions;

namespace HRManagement.Application.Validators.Permission
{
    public class UpdatePermissionDtoValidator : AbstractValidator<PermissionForUpdateDto>
    {
        public UpdatePermissionDtoValidator()
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
