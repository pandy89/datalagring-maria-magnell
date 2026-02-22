namespace ClassCloud.Application.Dtos.CourseStatus;

public record CourseStatusDto(
    int Id,
    string StatusType,
    byte[] RowVersion

    );

