namespace ClassCloud.Application.Dtos.Course;

public class CourseDto
{
    public string? CourseCode { get; set; }
    public string? CourseName { get; set; }
    public string? CourseDescription { get; set; }
    public DateTime? CreatedAtUtc { get; set; }
    public DateTime? UpdatedAtUtc { get; set; }
    public bool IsDeleted { get; set; }
    public byte[] RowVersion { get; set; } = [];
}
