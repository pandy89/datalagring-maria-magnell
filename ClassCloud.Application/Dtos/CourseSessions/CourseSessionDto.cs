using ClassCloud.Application.Dtos.Course;
using ClassCloud.Application.Dtos.Locations;

namespace ClassCloud.Application.Dtos.CourseSessions;

public record CourseSessionDto
(
    DateTime StartDate,
    DateTime EndDate,
    int MaxParticipants,
    DateTime CreateAtUtc,
    DateTime UpdatedAtUtc,
    CourseDto Course,
    LocationDto Location,
    byte[] RowVersion
);
