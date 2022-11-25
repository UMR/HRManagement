using AutoMapper;
using HRManagement.Application.Contracts;
using HRManagement.Application.Contracts.Persistence;
using HRManagement.Application.Dtos.Identities;
using HRManagement.Application.Features.Identities.Validators;
using HRManagement.Application.Options;
using HRManagement.Domain.Entities;
using MediatR;

namespace HRManagement.Application.Features.Identities.Commands
{
    public record RegisterCommand : IRequest<AuthResult>
    {
        public RegisterDto RegisterDto { get; set; }
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenGenerator _tokenGenerator;

        public RegisterCommandHandler(IUserRepository userRepository,
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

        public async Task<AuthResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            byte[] passwordHash, passwordSalt;
            var validator = new RegisterDtoValidator();
            var validationResult = await validator.ValidateAsync(request.RegisterDto);

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

            var userRequest = await _userRepository.GetUserByEmailAsync(request.RegisterDto.Email.ToLower());

            if (userRequest != null)
            {
                return new AuthResult
                {
                    Success = false,
                    Token = String.Empty,
                    RefreshToken = String.Empty,
                    Errors = new[] { "User with this email address already exist" }
                };
            }

            _passwordHasher.CreatePasswordHash(request.RegisterDto.Password, out passwordHash, out passwordSalt);

            var newUser = new User
            {
                FirstName = request.RegisterDto.FirstName,
                LastName = request.RegisterDto.LastName,
                Email = request.RegisterDto.Email.ToLower(),
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            var createdUser = await _userRepository.CreateUserAsync(newUser);
            var token = _tokenGenerator.GenerateToken(createdUser);

            var refreshToken = new RefreshToken
            {
                JwtId = token.Id,
                UserId = createdUser.Id,
                CreatedDate = DateTime.Now,
                ExpiryDate = DateTime.Now.AddMinutes(5)
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
