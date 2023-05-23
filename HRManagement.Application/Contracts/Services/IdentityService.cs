using HRManagement.Application.Contracts.Persistence;
using HRManagement.Application.Dtos.Identities;
using HRManagement.Application.Dtos.RefreshTokens;
using HRManagement.Application.Validators;
using HRManagement.Application.Validators.Identity;
using HRManagement.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HRManagement.Application.Contracts.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public IdentityService(IUserRepository userRepository,
            IRoleRepository roleRepository,
            IRefreshTokenRepository refreshTokenRepository,
            IPasswordHasher passwordHasher,
            ITokenGenerator tokenGenerator,
            TokenValidationParameters tokenValidationParameters)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
            _tokenValidationParameters = tokenValidationParameters;
        }

        public async Task<AuthResult> LoginAsync(LoginDto loginDto)
        {
            var validator = new LoginDtoValidator();
            var validationResult = await validator.ValidateAsync(loginDto);

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

            var userRequest = await _userRepository.GetUserByEmailAsync(loginDto.Email.ToLower());

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

            if (!_passwordHasher.VerifyPasswordHash(loginDto.Password, userRequest.PasswordHash, userRequest.PasswordSalt))
            {
                return new AuthResult
                {
                    Success = false,
                    Token = String.Empty,
                    RefreshToken = String.Empty,
                    Errors = new[] { "User/password combination is wrong" }
                };
            }

            //var roles = _roleRepository.get

            var token = _tokenGenerator.GenerateToken(userRequest);

            var refreshToken = new RefreshToken
            {
                JwtId = token.Id,
                UserId = userRequest.Id,
                CreatedDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMinutes(2)
            };

            await _refreshTokenRepository.CreateRefreshTokenAsync(refreshToken);

            return new AuthResult
            {
                Success = true,
                Token = token.Token,
                RefreshToken = refreshToken.Token
            };
        }

        public async Task<AuthResult> RefreshTokenAsync(RefreshTokenDto refreshTokenDto)
        {
            var validator = new RefreshTokenDtoValidator();
            var validationResult = await validator.ValidateAsync(refreshTokenDto);

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

            var validatedToken = GetPrincipalFromToken(refreshTokenDto.Token);

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

            var storedRefreshToken = await _refreshTokenRepository.GetRefreshTokenByTokenAsync(refreshTokenDto.RefreshToken);

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

            var refreshTokenToCreate = new RefreshToken
            {
                JwtId = token.Id,
                UserId = user.Id,
                CreatedDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMinutes(5)
            };

            await _refreshTokenRepository.CreateRefreshTokenAsync(refreshTokenToCreate);

            return new AuthResult
            {
                Success = true,
                Token = token.Token,
                RefreshToken = refreshTokenToCreate.Token
            };
        }

        public async Task<AuthResult> RegisterAsync(RegisterDto registerDto)
        {
            byte[] passwordHash, passwordSalt;
            var validator = new RegisterDtoValidator();
            var validationResult = await validator.ValidateAsync(registerDto);

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

            var userRequest = await _userRepository.GetUserByEmailAsync(registerDto.Email.ToLower());

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

            _passwordHasher.CreatePasswordHash(registerDto.Password, out passwordHash, out passwordSalt);

            var userToCreate = new User
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email.ToLower(),
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            var createdUser = await _userRepository.CreateUserAsync(userToCreate);
            var token = _tokenGenerator.GenerateToken(createdUser);

            var refreshTokenToCreate = new RefreshToken
            {
                JwtId = token.Id,
                UserId = createdUser.Id,
                CreatedDate = DateTime.Now,
                ExpiryDate = DateTime.Now.AddMinutes(5)
            };

            await _refreshTokenRepository.CreateRefreshTokenAsync(refreshTokenToCreate);

            return new AuthResult
            {
                Success = true,
                Token = token.Token,
                RefreshToken = refreshTokenToCreate.Token
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
