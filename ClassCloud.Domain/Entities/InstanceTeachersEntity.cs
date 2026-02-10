namespace ClassCloud.Domain.Entities;

public class InstanceTeachersEntity
{
    public int CourseSessionId { get; set; }
    public int TeacherId { get; set; }
    public byte[] RowVersion { get; set; } = null!;
}
