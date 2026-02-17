namespace ClassCloud.Application.Dtos.Course;

public record CourseDto
(
    string CourseCode,
    string CourseName,
    string CourseDescription,
    DateTime CreatedAtUtc,
    DateTime? UpdatedAtUtc,
    bool IsDeleted,
    byte[] RowVersion
);

