namespace HRManagement.Domain.Entities
{
    public class Role : BaseAuditableEntity
    {
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }

        public ICollection<Permission> Permissions { get; set; }
    }
}
