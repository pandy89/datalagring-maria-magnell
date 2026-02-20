using ClassCloud.Application.Dtos.Course;
using ClassCloud.Application.Dtos.Locations;
using System.ComponentModel.DataAnnotations;

namespace ClassCloud.Application.Dtos.CourseSessions;

public sealed record CreateCourseSessionDto
(
    DateTime StartDate,
    DateTime EndDate,
    int MaxParticipants,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    CourseDto Course,
    LocationDto Location,
    byte[] RowVersion

);
    

