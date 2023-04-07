namespace HRManagement.Infrastructure.Persistence.Configurations
{
    public class RoleClaimConfiguration : BaseEntityConfiguration<RoleClaim>
    {
        public override void Configure(EntityTypeBuilder<RoleClaim> builder)
        {
            builder.Property(o => o.ClaimType).HasMaxLength(256).IsRequired();
            builder.Property(o => o.ClaimValue).HasMaxLength(256).IsRequired();
            builder.Property(o => o.RoleId).IsRequired();
            builder.Property(b => b.Created).IsRequired(false);
            builder.Property(b => b.CreatedBy).IsRequired(false);
            builder.Property(b => b.LastModified).IsRequired(false);
            builder.Property(b => b.LastModifiedBy).IsRequired(false);
            builder.ToTable(nameof(RoleClaim));
        }

    }
}
