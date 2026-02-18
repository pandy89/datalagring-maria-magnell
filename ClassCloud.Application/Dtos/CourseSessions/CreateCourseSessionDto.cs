using System.ComponentModel.DataAnnotations;

namespace ClassCloud.Application.Dtos.CourseSessions;

public sealed record CreateCourseSessionDto
(
    DateTime StartDate,
    DateTime EndDate,
    int MaxParticipants, 
    string CourseName,
    int CourseId,
    string Location,
    int LocationId
);
    

