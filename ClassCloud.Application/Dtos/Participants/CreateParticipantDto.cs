using System.ComponentModel.DataAnnotations;

namespace ClassCloud.Application.Dtos.Participants;

public class CreateParticipantDto
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

    [MinLength(1)]
    [MaxLength(50)]
    public string PhoneNumber { get; set; } = null!;
}
