
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class TenantConfig : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> b)
        {
            b.ToTable("Tenants");

            b.HasKey(x => x.Id);

            b.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();

            b.HasIndex(x => x.IsActive);
        }
    }
}
