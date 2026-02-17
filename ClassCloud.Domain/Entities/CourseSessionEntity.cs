namespace ClassCloud.Domain.Entities;

public class CourseSessionEntity
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int MaxParticipants { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime UpdatedAtUtc { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsCanceled { get; set; }   
    
    public int CourseId { get; set; } //FK
    public CourseEntity Course { get; set; } = null!;
    
    public int LocationId { get; set; }  //FK
    public LocationEntity Location { get; set; } = null!;

    public ICollection<CourseRegistrationEntity> CourseRegistrations { get; set; } = [];

    public ICollection<TeacherCourseSessionEntity> TeacherCourseSessions { get; set; } = [];

    public byte[] RowVersion { get; set; } = null!;
}
