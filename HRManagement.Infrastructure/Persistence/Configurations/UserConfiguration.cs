namespace HRManagement.Infrastructure.Persistence.Configurations
{
    internal class UserConfiguration : BaseEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(o => o.FirstName).HasMaxLength(256).IsRequired();
            builder.Property(o => o.LastName).HasMaxLength(256).IsRequired();
            builder.Property(o => o.Email).HasMaxLength(256).IsRequired();            
            builder.Property(o => o.PasswordHash).IsRequired();
            builder.Property(o => o.PasswordSalt).IsRequired();            
            builder.Property(b => b.Created).IsRequired(false);
            builder.Property(b => b.CreatedBy).IsRequired(false);
            builder.Property(b => b.LastModified).IsRequired(false);
            builder.Property(b => b.LastModifiedBy).IsRequired(false);
        }

    }
}
