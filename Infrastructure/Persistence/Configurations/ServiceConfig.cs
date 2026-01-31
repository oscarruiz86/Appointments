
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class ServiceConfig : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> b)
        {
            b.ToTable("services");

            b.HasKey(x => x.Id);

            b.Property(x => x.Price)
                .HasPrecision(10, 2);

            b.HasIndex(x => x.TenantId);
        }
    }
}
