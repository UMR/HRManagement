namespace HRManagement.Domain.Entities
{
    public class Permission: BaseAuditableEntity
    {
        public string Name { get; set; }

        public ICollection<Role> Roles { get; set; }
    }
}
