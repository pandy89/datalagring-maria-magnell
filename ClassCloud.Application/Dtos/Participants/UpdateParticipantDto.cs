using System.ComponentModel.DataAnnotations;

namespace ClassCloud.Application.Dtos.Participants;

public record UpdateParticipantDto(
    [Required, MinLength(1), MaxLength(50)] string Email,
    [Required, MinLength(1), MaxLength(200)] string FirstName,
    [Required, MinLength(1), MaxLength(200)] string LastName,
    [MinLength(1), MaxLength(200)] string PhoneNumber,
    string DateTime,

    [Required] byte[] RowVersion
    );

