using System.ComponentModel.DataAnnotations;

namespace ClassCloud.Application.Dtos.Course;

public record UpdateCourseDto(
    [Required, MinLength(1), MaxLength(100)]string CourseName,
    [Required, MinLength(1), MaxLength(1000)] string CourseDescription,
    [Required] byte[] RowVersion
);
