using HRManagement.Application.Contracts.Infrastructure;

namespace HRManagement.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }

}
