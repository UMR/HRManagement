using AutoMapper;
using HRManagement.Application.Contracts;
using HRManagement.Application.Contracts.Persistence;
using HRManagement.Application.Dtos.Identities;
using HRManagement.Application.Dtos.Users;
using HRManagement.Application.Features.Users.Validators;
using HRManagement.Application.Options;
using HRManagement.Domain.Entities;
using MediatR;

namespace HRManagement.Application.Features.Users.Commands
{
    public record CreateUserCommand : IRequest<AuthResult>
    {
        public CreateUserDto CreateUserDto { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, AuthResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly JwtSettings _jwtSettings;

        public CreateUserCommandHandler(IUserRepository userRepository,
            IRefreshTokenRepository refreshTokenRepository, 
            IMapper mapper, IPasswordHasher passwordHasher,
            ITokenGenerator tokenGenerator,
            JwtSettings jwtSettings)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
            _jwtSettings = jwtSettings;
        }

        public async Task<AuthResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            byte[] passwordHash, passwordSalt;
            var validator = new CreateUserDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CreateUserDto);

            if (!validationResult.IsValid) 
            {
                return new AuthResult
                {
                    Success = false,
                    Token = String.Empty,
                    RefreshToken = String.Empty
                };
            }

            var userRequest = await _userRepository.GetUserByEmailAsync(request.CreateUserDto.Email.ToLower());

            if (userRequest != null)
            {
                return new AuthResult
                {
                    Errors = new[] { "User with this email address already exist" }
                };
            }

            _passwordHasher.CreatePasswordHash(request.CreateUserDto.Password, out passwordHash, out passwordSalt);

            var newUser = new User
            {
                FirstName = request.CreateUserDto.FirstName,
                LastName = request.CreateUserDto.LastName,
                Email = request.CreateUserDto.Email.ToLower(),
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            var createdUser = await _userRepository.CreateUserAsync(userRequest);
            var token = _tokenGenerator.GenerateToken(createdUser, String.Empty, String.Empty, String.Empty);

            var refreshToken = new RefreshToken
            {
                JwtId = token.Id,
                Id = createdUser.Id,
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
