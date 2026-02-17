using ClassCloud.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClassCloud.Infrastructure.Persistence.Configurations;

public class TeacherCourseSessionConfiguration : IEntityTypeConfiguration<TeacherCourseSessionEntity>
{
    public void Configure(EntityTypeBuilder<TeacherCourseSessionEntity> builder)
    {
        // Primary key
        builder.HasKey(x => new { x.TeacherId, x.CourseSessionId })
            .HasName("PK_TeacherCourseSessions_Id"); ;

        builder.HasOne(x => x.Teacher)
            .WithMany(x => x.TeacherCourseSessions)
            .HasForeignKey(x => x.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.CourseSession)
            .WithMany(x => x.TeacherCourseSessions)
            .HasForeignKey(x => x.CourseSessionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(e => e.RowVersion)
            .IsRowVersion();
    }
}
