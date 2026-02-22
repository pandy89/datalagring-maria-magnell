using ClassCloud.Application.Dtos.CourseSessions;
using ClassCloud.Application.Dtos.CourseStatus;
using ClassCloud.Application.Dtos.Participants;

namespace ClassCloud.Application.Dtos.CourseRegistrations;

public record CourseRegistrationDto(
    int Id,
    DateTime RegistrationDate,
    DateTime UpdatedAtUt,
    bool IsDeleted,
    ParticipantDto Participant,
    CourseSessionDto CourseSession,
    CourseStatusDto CourseStatus,

    byte[] RowVersion
    );

