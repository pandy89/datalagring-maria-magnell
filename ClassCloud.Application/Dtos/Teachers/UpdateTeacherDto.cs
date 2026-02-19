using System.ComponentModel.DataAnnotations;

namespace ClassCloud.Application.Dtos.Teachers;

public record UpdateTeacherDto(
    [Required, MinLength(1), MaxLength(255)] string Email,
    [Required, MinLength(1), MaxLength(100)] string FirstName,
    [Required, MinLength(1), MaxLength(100)] string LastName,
    [MinLength(1), MaxLength(100)] string Expertise,
    string DateTime,

    [Required] byte[] RowVersion
    );
