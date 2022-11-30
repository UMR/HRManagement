using FluentValidation;
using HRManagement.Application.Dtos.RefreshTokens;

namespace HRManagement.Application.Features.Identities.Validators
{
    public class RefreshTokenDtoValidator : AbstractValidator<RefreshTokenDto>
    {
        public RefreshTokenDtoValidator()
        {
            RuleFor(c => c.Token)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull();

            RuleFor(c => c.RefreshToken)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull();
              
        }
    }
}
