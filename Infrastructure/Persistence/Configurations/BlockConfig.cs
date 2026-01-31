
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class BlockConfig : IEntityTypeConfiguration<Block>
    {
        public void Configure(EntityTypeBuilder<Block> b)
        {
            b.ToTable("blocks");

            b.HasKey(x => x.Id);

            b.HasIndex(x => new { x.EmployeeId, x.StartAt });

            b.Property(x => x.Reason)
                .HasMaxLength(250);

            b.HasOne(x => x.Employee)
                .WithMany()
                .HasForeignKey(x => x.EmployeeId);
        }
    }
}
