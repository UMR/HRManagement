namespace HRManagement.Infrastructure.Persistence.Configurations
{
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.Property(o => o.PermissionId).IsRequired();
            builder.Property(o => o.RoleId).IsRequired();
            builder.ToTable(nameof(RolePermission));

            builder.HasKey(ur => new { ur.PermissionId, ur.RoleId });

            builder.HasOne(bc => bc.Role)
                .WithMany(b => b.RolePermissions)
                .HasForeignKey(bc => bc.RoleId);

            builder.HasOne(bc => bc.Permission)
                .WithMany(c => c.RolePermissions)
                .HasForeignKey(bc => bc.PermissionId);
        }

    }
}
