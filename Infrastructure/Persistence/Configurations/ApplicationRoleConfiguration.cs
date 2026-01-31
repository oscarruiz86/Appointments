
using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {

            builder.ToTable("Roles");

            builder.Property(x => x.Name)
                .HasMaxLength(100);

            builder.HasIndex(x => x.NormalizedName)
                .IsUnique();
        }
    }
}
