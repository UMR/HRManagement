using HRManagement.Application.Contracts;

namespace HRManagement.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }

}
