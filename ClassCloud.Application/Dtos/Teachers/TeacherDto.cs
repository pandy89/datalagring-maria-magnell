namespace ClassCloud.Application.Dtos.Teachers;

public record TeacherDto(
    int Id,
    string FirstName,
    string LastName,
    string Expertise,
    string Email,
    DateTime CreatedAtUtc,
    DateTime UpdatedAtUtc,
    bool IsDeleted,
    byte[] RowVersion
);

