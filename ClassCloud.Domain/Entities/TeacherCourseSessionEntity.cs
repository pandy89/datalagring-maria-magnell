namespace ClassCloud.Domain.Entities;

public class TeacherCourseSessionEntity
{
    public int Id { get; set; }
    public int CourseSessionId { get; set; } // FK

    public CourseSessionEntity CourseSession { get; set; } = null!;
    public int TeacherId { get; set; } // FK

    public TeacherEntity Teacher { get; set; } = null!;

    public byte[] RowVersion { get; set; } = null!;
}
