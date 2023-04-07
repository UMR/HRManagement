namespace HRManagement.Infrastructure.Persistence.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(r => r.Token);
            builder.Property(r => r.JwtId).HasMaxLength(512).IsRequired();
            builder.Property(r => r.CreatedDate).IsRequired();
            builder.Property(r => r.ExpiryDate).IsRequired();
            builder.Property(r => r.Used).IsRequired();
            builder.Property(r => r.Invalidated).IsRequired();
            builder.Property(r => r.UserId).IsRequired();
            builder.ToTable(nameof(RefreshToken));
        }
    }
}
