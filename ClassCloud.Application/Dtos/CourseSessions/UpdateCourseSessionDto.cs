namespace ClassCloud.Application.Dtos.CourseSessions;

public record UpdateCourseSessionDto
(
    DateTime StartDate,
    DateTime EndDate,
    int MaxParticipants,
    int LocationId,
    byte[] RowVersion
);
