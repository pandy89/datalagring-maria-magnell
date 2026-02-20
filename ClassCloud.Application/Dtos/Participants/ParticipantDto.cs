namespace ClassCloud.Application.Dtos.Participants;

public record ParticipantDto(
    string Email,
    string FirstName,
    string LastName,
    string? PhoneNumber,
    byte[] RowVersion
);

