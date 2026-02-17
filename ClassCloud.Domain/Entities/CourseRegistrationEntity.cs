namespace ClassCloud.Domain.Entities;

public class CourseRegistrationEntity
{
    public int Id { get; set; }
    public DateTime RegistrationDate { get; set; }
    public DateTime UpdatedAtUtc { get; set; }
    public bool IsDeleted { get; set; }

    public int ParticipantId { get; set; } //FK
    public ParticipantEntity Participant { get; set; } = null!;

    public int CourseSessionId { get; set; } //FK
    public CourseSessionEntity CourseSession { get; set; } = null!;

    public int CourseStatusId { get; set; } //FK
    public CourseStatusEntity CourseStatus { get; set; } = null!;

    public byte[] RowVersion { get; set; } = null!;
}
