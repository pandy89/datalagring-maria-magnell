using System.ComponentModel.DataAnnotations;

namespace ClassCloud.Application.Dtos.Course;

public record UpdateCourseDto(
    [Required, MinLength(1), MaxLength(50)]string CourseName,
    [Required, MinLength(1), MaxLength(200)] string CourseDescription,
    [Required] byte[] RowVersion
);
