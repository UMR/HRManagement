namespace HRManagement.Domain.Entities
{
    public class Role : BaseAuditableEntity
    {
        public string Name { get; set; }

        public string NormalizedName { get; set; }

        public string ConcurrencyStamp { get; set; }

        public ICollection<User> Users { get; set; }

        public ICollection<RoleClaim> RoleClaims { get; set; }

    }
}
