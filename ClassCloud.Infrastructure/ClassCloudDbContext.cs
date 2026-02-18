using ClassCloud.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClassCloud.Infrastructure;

public class ClassCloudDbContext(DbContextOptions<ClassCloudDbContext> options) : DbContext(options)
{
    public DbSet<CourseEntity> Courses { get; set; }
    public DbSet<CourseStatusEntity> CourseStatus { get; set; }
    public DbSet<CourseSessionEntity> CourseSessions { get; set; }
    public DbSet<LocationEntity> Locations { get; set; }
    public DbSet<ParticipantEntity> Participants { get; set; }
    public DbSet<TeacherCourseSessionEntity> TeacherCourseSessions { get; set; }
    public DbSet<TeacherEntity> Teachers { get; set; }
    public DbSet<CourseRegistrationEntity> CourseRegistrations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClassCloudDbContext).Assembly);
    }
}
