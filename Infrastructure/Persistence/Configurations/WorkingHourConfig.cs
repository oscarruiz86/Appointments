using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class WorkingHourConfig : IEntityTypeConfiguration<WorkingHour>
    {
        public void Configure(EntityTypeBuilder<WorkingHour> b)
        {
            b.ToTable("working_hours");

            b.HasKey(x => x.Id);

            b.HasIndex(x => new { x.EmployeeId, x.Weekday });

            b.HasOne(x => x.Employee)
                .WithMany()
                .HasForeignKey(x => x.EmployeeId);
        }
    }
}
