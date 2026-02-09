using ClassCloud.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClassCloud.Infrastructure.Persistence.Configurations;

public class CourseConfigurations : IEntityTypeConfiguration<CourseEntity>
{
    public void Configure(EntityTypeBuilder<CourseEntity> builder)
    {
        //Primär nyckel
        builder.HasKey(e => e.Id)
            .HasName("PK_Course_Id");

        builder.HasIndex(e => e.CourseCode)
            .IsUnique();

        builder.Property(e => e.CourseName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.CourseDescription)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(e => e.CreatedAtUtc)
            .HasColumnType("datetime2(0)")
            .HasDefaultValue("(SYSUTCDATETIME())", "DF_Course_CreatedAtUtc");

        builder.Property(e => e.UpdatedAtUtc)
            .HasColumnType("datetime2(0)")
            .HasDefaultValue("(SYSUTCDATETIME())", "DF_Course_UpdatedAtUtc");

        builder.Property(e => e.IsDeleted)
            .HasDefaultValue(false);

        builder.Property(e => e.RowVersion)
            .IsRowVersion();
    }
}