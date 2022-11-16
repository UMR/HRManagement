namespace HRManagement.Application.Contracts.Infrastructure
{
    public interface ICurrentUserService
    {
        int? UserId { get; }
    }

}
