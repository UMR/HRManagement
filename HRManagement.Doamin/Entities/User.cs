namespace HRManagement.Domain.Entities;

public class User : BaseAuditableEntity
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public byte[] PasswordHash { get; set; }

    public byte[] PasswordSalt { get; set; }

    public ICollection<Role> Roles { get; set; }

}
