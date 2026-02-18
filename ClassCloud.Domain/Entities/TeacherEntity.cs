namespace ClassCloud.Domain.Entities;

public class TeacherEntity
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Expertise { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTime CreatedAtUtc { get; set; }
    public DateTime UpdatedAtUtc { get; set; }
    public bool IsDeleted { get; set; }
    public byte[] RowVersion { get; set; } = null!;

    public ICollection<TeacherCourseSessionEntity> TeacherCourseSessions { get; set; } = [];
}
