using FluentValidation;
using HRManagement.Application.Dtos.Permissions;

namespace HRManagement.Application.Validators.Permission
{
    public class CreatePermissionDtoValidator : AbstractValidator<PermissionForCreateDto>
    {
        public CreatePermissionDtoValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(128).WithMessage("{PropertyName} must not exceed 128 characters");

        }
    }
}
