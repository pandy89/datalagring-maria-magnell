using ClassCloud.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClassCloud.Infrastructure.Persistence.Configurations;

public class CourseConfigurations : IEntityTypeConfiguration<CourseEntity>
{
    public void Configure(EntityTypeBuilder<CourseEntity> builder)
    {
        // Primary key
        builder.HasKey(e => e.Id)
            .HasName("PK_Courses_Id");

        builder.HasIndex(e => e.CourseCode)
            .IsUnique()
            .HasDatabaseName("UQ_Courses_CourseCode");

        builder.Property(e => e.CourseName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.CourseDescription)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(e => e.CreatedAtUtc)
            .HasColumnType("datetime2(0)")
            .HasDefaultValueSql("(SYSUTCDATETIME())", "DF_Courses_CreatedAtUtc")
            .ValueGeneratedOnAdd();

        builder.Property(e => e.UpdatedAtUtc)
            .HasColumnType("datetime2(0)")
            .HasDefaultValueSql("(SYSUTCDATETIME())", "DF_Courses_UpdatedAtUtc")
            .ValueGeneratedOnAdd();

        builder.Property(e => e.IsDeleted)
            .HasDefaultValue(false);

        builder.Property(e => e.RowVersion)
            .IsRowVersion();
    }
}