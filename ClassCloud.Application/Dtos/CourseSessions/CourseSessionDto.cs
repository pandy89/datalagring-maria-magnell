using ClassCloud.Application.Dtos.Course;
using ClassCloud.Application.Dtos.Locations;

namespace ClassCloud.Application.Dtos.CourseSessions;

public sealed record CourseSessionDto
(
    int Id,
    DateTime StartDate,
    DateTime EndDate,
    int MaxParticipants,
    DateTime CreateAtUtc,
    DateTime UpdatedAtUtc,
    bool IsDeleted,
    bool IsCanceled,
    CourseDto Course,
    LocationDto Location,

    byte[] RowVersion
);
