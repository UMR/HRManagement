using HRManagement.Domain.Common;

namespace HRManagement.Domain.Entities
{
    public class User: BaseAuditableEntity
    {        
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
       
    }
}
