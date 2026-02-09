using ClassCloud.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClassCloud.Infrastructure.Persistence.Configurations;

public class CourseStatusConfiguration : IEntityTypeConfiguration<CourseStatusEntity>
{
    public void Configure(EntityTypeBuilder<CourseStatusEntity> builder)
    {
        builder.HasKey(e => e.Id )
            .HasName("PK_CourseStatus_Id");

        builder.Property(e => e.StatusType)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.RowVersion)
            .IsRowVersion();
    }
}
