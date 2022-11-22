namespace HRManagement.Domain.Entities;

public class User: BaseAuditableEntity
{        
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public byte[] PasswordHash { get; set; }

    public byte[] PasswordSalt { get; set; }

}
