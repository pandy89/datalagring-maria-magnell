namespace ClassCloud.Application.Dtos.CourseRegistrations;

public record UpdateCourseRegistrationDto(
    int ParticipantId,
    int CourseSessionId,
    int CourseStatusId,

    byte[] RowVersion
);