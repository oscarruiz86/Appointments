
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class AppointmentConfig : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> b)
        {
            b.ToTable("appointments");

            b.HasKey(x => x.Id);

            b.HasIndex(x => new { x.EmployeeId, x.StartAt });

            b.HasOne(x => x.Status)
                .WithMany(s => s.Appointments)
                .HasForeignKey(x => x.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(x => x.Employee)
                .WithMany(e => e.Appointments)
                .HasForeignKey(x => x.EmployeeId);

            b.HasOne(x => x.Customer)
                .WithMany(c => c.Appointments)
                .HasForeignKey(x => x.CustomerId);

            b.HasOne(x => x.Service)
                .WithMany()
                .HasForeignKey(x => x.ServiceId);
        }
    }
}