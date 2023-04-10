namespace HRManagement.Infrastructure.Persistence.Configurations
{
    public class PermissionConfiguration : BaseEntityConfiguration<Permission>
    {
        public override void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.Property(o => o.Name).HasMaxLength(256).IsRequired();            
            builder.Property(b => b.Created).IsRequired(false);
            builder.Property(b => b.CreatedBy).IsRequired(false);
            builder.Property(b => b.LastModified).IsRequired(false);
            builder.Property(b => b.LastModifiedBy).IsRequired(false);
            builder.ToTable(nameof(Permission));
        }

    }
}
