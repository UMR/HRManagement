using AutoMapper;
using HRManagement.Application.Contracts.Persistence;
using HRManagement.Application.Contracts;
using HRManagement.Application.Dtos.Identities;
using HRManagement.Application.Features.Identities.Validators;
using HRManagement.Application.Options;
using HRManagement.Domain.Entities;
using MediatR;

namespace HRManagement.Application.Features.Identities.Commands
{
    public record LoginCommand : IRequest<AuthResult>
    {
        public LoginDto LoginDto { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenGenerator _tokenGenerator;

        public LoginCommandHandler(IUserRepository userRepository,
            IRefreshTokenRepository refreshTokenRepository,
            IMapper mapper, IPasswordHasher passwordHasher,
            ITokenGenerator tokenGenerator,
            JwtSettings jwtSettings)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<AuthResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {            
            var validator = new LoginDtoValidator();
            var validationResult = await validator.ValidateAsync(request.LoginDto);

            if (!validationResult.IsValid)
            {
                return new AuthResult
                {
                    Success = false,
                    Token = String.Empty,
                    RefreshToken = String.Empty,
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var userRequest = await _userRepository.GetUserByEmailAsync(request.LoginDto.Email.ToLower());

            if (userRequest == null)
            {
                return new AuthResult
                {
                    Success = false,
                    Token = String.Empty,
                    RefreshToken = String.Empty,
                    Errors = new[] { "User does not exist" }
                };
            }

            if (!_passwordHasher.VerifyPasswordHash(request.LoginDto.Password, userRequest.PasswordHash, userRequest.PasswordSalt)) 
            {
                return new AuthResult
                {
                    Success = false,
                    Token = String.Empty,
                    RefreshToken = String.Empty,
                    Errors = new[] { "User/password combination is wrong" }
                };
            }
            
            var token = _tokenGenerator.GenerateToken(userRequest);

            var refreshToken = new RefreshToken
            {
                JwtId = token.Id,
                UserId = userRequest.Id,
                CreatedDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6)
            };

            await _refreshTokenRepository.CreateRefreshTokenAsync(refreshToken);

            return new AuthResult
            {
                Success = true,
                Token = token.Token,
                RefreshToken = refreshToken.Token
            };
        }

    }
}
