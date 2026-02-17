using ClassCloud.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClassCloud.Infrastructure.Persistence.Configurations;

public class CourseSessionsConfiguration : IEntityTypeConfiguration<CourseSessionEntity>
{
    public void Configure(EntityTypeBuilder<CourseSessionEntity> builder)
    {
        // Primary key
        builder.HasKey(e => e.Id)
            .HasName("PK_CourseSessions_Id");

        builder.Property(e => e.StartDate)
           .HasColumnType("datetime2(0)")
           .HasDefaultValue("(SYSUTCDATETIME())", "DF_CourseSessions_StartDate");

        builder.Property(e => e.EndDate)
            .HasColumnType("datetime2(0)")
            .HasDefaultValue("(SYSUTCDATETIME())", "DF_CourseSessions_EndDate");

        builder.ToTable("CourseSessions", t =>
        {
            t.HasCheckConstraint(
                $"CK_CourseSession_{nameof(CourseSessionEntity.MaxParticipants)}",
                $"[{nameof(CourseSessionEntity.MaxParticipants)}] > 0"
            );
        });

        builder.Property(e => e.CreatedAtUtc)
            .HasColumnType("datetime2(0)")
            .HasDefaultValue("(SYSUTCDATETIME())", "DF_CourseSessions_CreatedAtUtc");

        builder.Property(e => e.UpdatedAtUtc)
            .HasColumnType("datetime2(0)")
            .HasDefaultValue("(SYSUTCDATETIME())", "DF_CourseSessions_UpdatedAtUtc");

        builder.Property(e => e.IsDeleted)
            .HasDefaultValue(false);

        builder.Property(e => e.IsCanceled)
            .HasDefaultValue(false);

        builder.Property(e => e.RowVersion)
            .IsRowVersion();


        // Relation
        builder.HasOne(cs => cs.Course)
            .WithMany(c => c.CourseSessions)
            .HasForeignKey(cs => cs.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(cs => cs.Location)
            .WithMany(l => l.CourseSessions)
            .HasForeignKey(cs => cs.LocationId)
            .OnDelete(DeleteBehavior.Restrict);

        
    }
}
