using System.ComponentModel.DataAnnotations;

namespace ClassCloud.Application.Dtos.Participants;

public record UpdateParticipantDto(
    [Required]
    [MinLength(1)]
    [MaxLength(255)] 
    string Email,

    [Required]
    [MinLength(1)] 
    [MaxLength(100)] 
    string FirstName,

    [Required] 
    [MinLength(1)] 
    [MaxLength(100)] 
    string LastName,

    [MinLength(1)] 
    [MaxLength(50)] 
    string PhoneNumber,

    [Required]
    byte[] RowVersion
    );

