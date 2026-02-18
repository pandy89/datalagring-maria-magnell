namespace ClassCloud.Application.Dtos.Teachers;

public record TeacherDto(
    string FirstName,
    string LastName,
    string Expertise,
    string Email,
    DateTime CreatedAtUtc,
    DateTime UpdatedAtUtc,
    bool IsDeleted,
    byte[] RowVersion
);

