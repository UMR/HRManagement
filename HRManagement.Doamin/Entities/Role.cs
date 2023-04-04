namespace HRManagement.Domain.Entities
{
    public class Role : BaseAuditableEntity
    {
        public string RoleName { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
