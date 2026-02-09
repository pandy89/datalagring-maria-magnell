using ClassCloud.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClassCloud.Infrastructure.Persistence.Configurations;

public class InstanceTeachersConfiguration : IEntityTypeConfiguration<InstanceTeachersEntity>
{
    public void Configure(EntityTypeBuilder<InstanceTeachersEntity> builder)
    {
        builder.HasKey(e => e.CourseSessionId);
        builder.HasKey(e => e.TeacherId);

        builder.Property(e => e.RowVersion)
            .IsRowVersion();
    }
}
