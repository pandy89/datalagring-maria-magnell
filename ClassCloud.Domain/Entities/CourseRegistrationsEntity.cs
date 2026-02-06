namespace ClassCloud.Domain.Entities;

public class CourseRegistrationsEntity
{
    public int Id { get; set; }
    public DateTime RegistrationDate { get; set; }
    public DateTime UpdatedAtUtc { get; set; }
    public bool IsDeleted { get; set; }

    public int ParticipantId { get; set; }
    public int CourseSessionId { get; set; }
    public int CourseStatusId { get; set; }
}
