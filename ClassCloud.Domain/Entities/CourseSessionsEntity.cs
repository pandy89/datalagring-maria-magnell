namespace ClassCloud.Domain.Entities;

public class CourseSessionsEntity
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int MaxParticipants { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime UpdatedAtUtc { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsCanceled { get; set; }

    public int LocationId { get; set; }
    public int CourseId { get; set; }

    public Byte[] RowVersion { get; set; } = null!;
}
