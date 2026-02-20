namespace ClassCloud.Application.Dtos.Course;

public record CourseDto
(
    int id,
    string CourseCode,
    string CourseName,
    string CourseDescription,
    DateTime CreatedAtUtc,
    DateTime? UpdatedAtUtc,
    bool IsDeleted,
    byte[] RowVersion
);

