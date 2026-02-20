using System.ComponentModel.DataAnnotations;

namespace ClassCloud.Application.Dtos.Locations;

public class CreateLocationDto
{
    [Required]
    [MinLength(1)]
    [MaxLength(100)]
    public string Name { get; set; } = null!;


}
