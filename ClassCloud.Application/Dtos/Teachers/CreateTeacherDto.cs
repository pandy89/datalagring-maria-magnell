using System.ComponentModel.DataAnnotations;

namespace ClassCloud.Application.Dtos.Teachers;

public class CreateTeacherDto
{
    [Required]
    [MinLength(1)]
    [MaxLength(255)]
    public string Email { get; set; } = null!;

    [Required]
    [MinLength(1)]
    [MaxLength(100)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MinLength(1)]
    [MaxLength(100)]
    public string LastName { get; set; } = null!;

    [Required]
    [MinLength(1)]
    [MaxLength(50)]
    public string Expertise { get; set; } = null!;

}
