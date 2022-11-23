using HRManagement.Application.Dtos.Tokens;
using HRManagement.Domain.Entities;

namespace HRManagement.Application.Contracts
{
    public interface ITokenGenerator
    {
        TokenResult GenerateToken(User user);        
    }
}
