using ClassCloud.Application.Dtos.Course;
using ClassCloud.Application.Dtos.Locations;

namespace ClassCloud.Application.Dtos.CourseSessions;

public record CreateCourseSessionDto
(
    DateTime StartDate,
    DateTime EndDate,
    int MaxParticipants,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    int CourseId,
    int LocationId
);


