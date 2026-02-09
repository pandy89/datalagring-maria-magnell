using ClassCloud.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClassCloud.Infrastructure.Persistence.Configurations;

public class CourseSessionsConfiguration : IEntityTypeConfiguration<CourseSessionsEntity>
{
    public void Configure(EntityTypeBuilder<CourseSessionsEntity> builder)
    {
        builder.HasKey(e => e.Id)
            .HasName("PK_CourseSessions_Id");

        builder.Property(e => e.StartDate)
           .HasColumnType("datetime2(0)")
           .HasDefaultValue("(SYSUTCDATETIME())", "DF_CourseSession_StartDate");

        builder.Property(e => e.EndDate)
            .HasColumnType("datetime2(0)")
            .HasDefaultValue("(SYSUTCDATETIME())", "DF_CourseSession_EndDate");

        builder.Property(e => e.MaxParticipants)
            .HasDefaultValue(null);

        builder.Property(e => e.CreatedAtUtc)
            .HasColumnType("datetime2(0)")
            .HasDefaultValue("(SYSUTCDATETIME())", "DF_CourseSession_CreatedAtUtc");

        builder.Property(e => e.UpdatedAtUtc)
            .HasColumnType("datetime2(0)")
            .HasDefaultValue("(SYSUTCDATETIME())", "DF_CourseSession_UpdatedAtUtc");

        builder.Property(e => e.IsDeleted)
            .HasDefaultValue(false);

        builder.Property(e => e.IsCanceled)
            .HasDefaultValue(false);

        builder.Property(e => e.LocationId);

        builder.Property(e => e.CourseId);

        builder.Property(e => e.RowVersion)
            .IsRowVersion();
    }
}
