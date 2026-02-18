namespace ClassCloud.Application.Dtos.Participants;

public record ParticipantDto(
    string Email,
    string FirstName,
    string LastName,
    string? PhoneNumber,
    DateTime CreateAtUtc,
    DateTime UpdatedAtUtc,
    bool IsDeleted,
    byte[] RowVersion
);

