namespace HRManagement.Infrastructure.Persistence.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.Property(o => o.UserId).IsRequired();
            builder.Property(o => o.RoleId).IsRequired();
            builder.ToTable(nameof(UserRole));

            builder.HasKey(ur => new { ur.UserId, ur.RoleId });

            builder.HasOne(bc => bc.Role )
                .WithMany(b => b.Users)
                .HasForeignKey(bc => bc.RoleId);

            builder.HasOne(bc => bc.User)
                .WithMany(c => c.Roles)
                .HasForeignKey(bc => bc.UserId);
        }

    }
}
