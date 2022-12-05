using AutoMapper;
using HRManagement.Application.Contracts;
using HRManagement.Application.Contracts.Persistence;
using HRManagement.Application.Dtos.Identities;
using HRManagement.Application.Dtos.RefreshTokens;
using HRManagement.Application.Features.Identities.Validators;
using HRManagement.Application.Options;
using HRManagement.Domain.Entities;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HRManagement.Application.Features.Identities.Commands
{
    public record RefreshTokenCommand : IRequest<AuthResult>
    {
        public RefreshTokenDto RefreshTokenDto { get; set; }
    }

    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public RefreshTokenCommandHandler(IUserRepository userRepository,
            IRefreshTokenRepository refreshTokenRepository,
            IMapper mapper, IPasswordHasher passwordHasher,
            ITokenGenerator tokenGenerator,
            JwtSettings jwtSettings,
            TokenValidationParameters tokenValidationParameters
            )
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
            _tokenValidationParameters = tokenValidationParameters;
        }

        public async Task<AuthResult> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var validator = new RefreshTokenDtoValidator();
            var validationResult = await validator.ValidateAsync(request.RefreshTokenDto);

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

            var validatedToken = GetPrincipalFromToken(request.RefreshTokenDto.Token);

            if (validatedToken == null)
            {
                return new AuthResult
                {
                    Success = false,
                    Token = String.Empty,
                    RefreshToken = String.Empty,
                    Errors = new[] { "Invalid Token" } 
                };
            }

            var expiryDateUnix = long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

            var expiryDateUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(expiryDateUnix);

            if (expiryDateUtc > DateTime.UtcNow)
            {
                return new AuthResult 
                {
                    Success = false,
                    Token = String.Empty,
                    RefreshToken = String.Empty,
                    Errors = new[] { "This token has not expired yet" } 
                };
            }

            var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            var storedRefreshToken = await _refreshTokenRepository.GetRefreshTokenByTokenAsync(request.RefreshTokenDto.RefreshToken);

            if (storedRefreshToken == null)
            {
                return new AuthResult
                {
                    Success = false,
                    Token = String.Empty,
                    RefreshToken = String.Empty,
                    Errors = new[] { "This refresh token does not exist" } 
                };
            }

            if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
            {
                return new AuthResult 
                {
                    Success = false,
                    Token = String.Empty,
                    RefreshToken = String.Empty,
                    Errors = new[] { "This refresh token has expired" } 
                };
            }

            if (storedRefreshToken.Invalidated)
            {
                return new AuthResult 
                {
                    Success = false,
                    Token = String.Empty,
                    RefreshToken = String.Empty,
                    Errors = new[] { "This refresh token has been invalidated" } 
                };
            }

            if (storedRefreshToken.Used)
            {
                return new AuthResult 
                {
                    Success = false,
                    Token = String.Empty,
                    RefreshToken = String.Empty,
                    Errors = new[] { "This refresh token has been used" } 
                };
            }

            if (storedRefreshToken.JwtId != jti)
            {
                return new AuthResult 
                {
                    Success = false,
                    Token = String.Empty,
                    RefreshToken = String.Empty,
                    Errors = new[] { "This refresh token does not match this jwt" } 
                };
            }

            storedRefreshToken.Used = true;
            await _refreshTokenRepository.UpdateRefreshTokenAsync(storedRefreshToken);

            var user = await _userRepository.GetUserByIdAsync(Convert.ToInt32(validatedToken.Claims.Single(x => x.Type == "id").Value));

            var token = _tokenGenerator.GenerateToken(user);

            var refreshToken = new RefreshToken
            {
                JwtId = token.Id,
                UserId = user.Id,
                CreatedDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMinutes(5)
            };

            await _refreshTokenRepository.CreateRefreshTokenAsync(refreshToken);

            return new AuthResult
            {
                Success = true,
                Token = token.Token,
                RefreshToken = refreshToken.Token
            };
        }

        private ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);
                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                {
                    return null;
                }

                return principal;
            }
            catch
            {
                return null;
            }

        }

        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

        }

    }
}
