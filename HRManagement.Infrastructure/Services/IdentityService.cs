namespace HRManagement.Infrastructure.Services
{
    public class IdentityService 
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public IdentityService(IUserRepository userRepository, IMapper mapper, TokenValidationParameters tokenValidationParameters)
        {
            _userRepository = userRepository;
        }
        //public async Task<AuthenticationResult> RegisterAsync(CreateUserDto createUserDto )
        //{
        //    var existingUser = await _userRepository.GetUserByEmailAsync(createUserDto.Email.ToLower());

        //    if (existingUser != null)
        //    {
        //        return new AuthenticationResult
        //        {
        //            Errors = new[] { "User with this email address already exist" }
        //        };
        //    }

        //    var newUser = _mapper.Map<User>(createUserDto);

        //    var createdUser = await _userRepository.CreateUserAsync(newUser);

        //    if (createdUser > 0)
        //    {
        //        return new AuthenticationResult
        //        {
        //            Errors = createdUser.Errors.Select(x => x.Description)
        //        };
        //    }

        //    return await GenerateAuthenticationResultForUserAsync(createdUser);
        //}

        //public async Task<AuthenticationResult> LoginAsync(string email, string password)
        //{
        //    var user = await _userRepository.GetUserByEmailAsync(email);

        //    if (user == null)
        //    {
        //        return new AuthenticationResult
        //        {
        //            Errors = new[] { "User does not exist" }
        //        };
        //    }

        //    var userHasValidPassword = await _userRepository.CheckPasswordAsync(user, password);

        //    if (!userHasValidPassword)
        //    {
        //        return new AuthenticationResult
        //        {
        //            Errors = new[] { "User/password combination is wrong" }
        //        };
        //    }

        //    return await GenerateAuthenticationResultForUserAsync(user);
        //}

        //private async Task<AuthenticationResult> GenerateAuthenticationResultForUserAsync(User user)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new[] {
        //            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
        //            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //            new Claim(JwtRegisteredClaimNames.Email, user.Email),
        //            new Claim("id", user.Id)
        //        }),
        //        Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifeTime),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };

        //    var token = tokenHandler.CreateToken(tokenDescriptor);

        //    var refreshToken = new RefreshToken
        //    {
        //        JwtId = token.Id,
        //        UserId = user.Id,
        //        CreatedDate = DateTime.UtcNow,
        //        ExpiryDate = DateTime.UtcNow.AddMonths(6)
        //    };

        //    await _dataContext.RefreshTokens.AddAsync(refreshToken);
        //    await _dataContext.SaveChangesAsync();

        //    return new AuthenticationResult
        //    {
        //        Success = true,
        //        Token = tokenHandler.WriteToken(token),
        //        RefreshToken = refreshToken.Token
        //    };
        //}

        //public async Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken)
        //{
        //    var validatedToken = GetPrincipalFromToken(token);

        //    if (validatedToken == null)
        //    {
        //        return new AuthenticationResult { Errors = new[] { "Invalid Token" } };
        //    }

        //    var expiryDateUnix = long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

        //    var expiryDateUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(expiryDateUnix);

        //    if (expiryDateUtc > DateTime.UtcNow)
        //    {
        //        return new AuthenticationResult { Errors = new[] { "This token has not expired yet" } };
        //    }

        //    var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

        //    var storedRefreshToken = await _dataContext.RefreshTokens.SingleOrDefaultAsync(x => x.Token == refreshToken);

        //    if (storedRefreshToken == null)
        //    {
        //        return new AuthenticationResult { Errors = new[] { "This refresh token does not exist" } };
        //    }

        //    if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
        //    {
        //        return new AuthenticationResult { Errors = new[] { "This refresh token has expired" } };
        //    }

        //    if (storedRefreshToken.Invalidated)
        //    {
        //        return new AuthenticationResult { Errors = new[] { "This refresh token has been invalidated" } };
        //    }

        //    if (storedRefreshToken.Used)
        //    {
        //        return new AuthenticationResult { Errors = new[] { "This refresh token has been used" } };
        //    }

        //    if (storedRefreshToken.JwtId != jti)
        //    {
        //        return new AuthenticationResult { Errors = new[] { "This refresh token does not match this jwt" } };
        //    }

        //    storedRefreshToken.Used = true;
        //    _dataContext.RefreshTokens.Update(storedRefreshToken);
        //    await _dataContext.SaveChangesAsync();

        //    var user = await _userRepository.GetUserByIdAsync(Convert.ToInt32(validatedToken.Claims.Single(x => x.Type == "id").Value));
        //    return await GenerateAuthenticationResultForUserAsync(user);
        //}

        //private ClaimsPrincipal GetPrincipalFromToken(string token)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();

        //    try
        //    {
        //        var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);
        //        if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
        //        {
        //            return null;
        //        }

        //        return principal;
        //    }
        //    catch
        //    {
        //        return null;
        //    }

        //}

        //private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        //{
        //    return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
        //        jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

        //}
    }
}
