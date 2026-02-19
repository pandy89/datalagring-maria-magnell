using System.ComponentModel.DataAnnotations;

namespace ClassCloud.Application.Dtos.Course;

public class CreateCourseDto
{
    [Required]
    [MinLength(1)]
    [MaxLength(20)]
    public string CourseCode { get; set; } = null!;

    [Required]
    [MinLength(1)]
    [MaxLength(100)]
    public string CourseName { get; set; } = null!;

    [Required]
    [MinLength(1)]
    [MaxLength(1000)]
    public string CourseDescription { get; set; } = null!;

}
