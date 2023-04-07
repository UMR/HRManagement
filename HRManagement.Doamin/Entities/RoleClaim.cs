namespace HRManagement.Domain.Entities
{
    public class RoleClaim: BaseAuditableEntity
    {
        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }

    }
}
