namespace ClassCloud.Application.Dtos.Participants;

public record ParticipantDto(
    int Id,
    string Email,
    string FirstName,
    string LastName,
    string? PhoneNumber,
    byte[] RowVersion
);

