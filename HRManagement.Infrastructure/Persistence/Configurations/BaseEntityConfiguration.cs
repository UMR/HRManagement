namespace HRManagement.Infrastructure.Persistence.Configurations
{
    public abstract class BaseEntityConfiguration<TEntityType> : IEntityTypeConfiguration<TEntityType>
   where TEntityType : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntityType> builder)
        {
            builder.HasKey(b => b.Id);            
        }
    }
}
