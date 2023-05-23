namespace HRManagement.Domain.Entities
{
    public class Role : BaseAuditableEntity
    {
        public string Name { get; set; }

        public ICollection<UserRole> Users { get; set; }

        public ICollection<RoleClaim> RoleClaims { get; set; }

        public ICollection<RolePermission> Permissions { get; set; }

    }
}
