using ClassCloud.Application.Dtos.CourseSessions;
using ClassCloud.Application.Dtos.CourseStatus;
using ClassCloud.Application.Dtos.Participants;

namespace ClassCloud.Application.Dtos.CourseRegistrations;

public record CreateCourseRegistrationDto(
    DateTime RegistrationDate,
    DateTime UpdatedAtUtc,
    bool IsDeleted,
    int ParticipantId,
    int CourseSessionId,
    int CourseStatusId    
);
