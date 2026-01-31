using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class AppointmentStatusConfig : IEntityTypeConfiguration<AppointmentStatus>
    {
        public void Configure(EntityTypeBuilder<AppointmentStatus> b)
        {
            b.ToTable("appointment_status");

            b.HasKey(x => x.Id);

            b.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            b.Property(x => x.Color)
                .HasMaxLength(20);

            b.HasIndex(x => new { x.TenantId, x.Name })
                .IsUnique();

            b.HasIndex(x => x.TenantId);
        }
    }
}
