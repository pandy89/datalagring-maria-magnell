using ClassCloud.Application.Abstractions.Persistence;
using ClassCloud.Application.Common.Errors;
using ClassCloud.Application.Dtos.CourseSessions;
using ClassCloud.Application.Mappers;
using ClassCloud.Domain.Entities;

namespace ClassCloud.Application.Services;

public class CourseSessionService(ICourseSessionRepository coursesessionRepository)
{
    // Get one selected coursesession by Id.
    public async Task<ErrorOr<CourseSessionDto>> GetOneCourseSessionAsync(int id, CancellationToken ct = default)
    {
        var courseSession = await coursesessionRepository.GetOneAsync(
            where: cs => cs.Id == id,
            select: CourseSessionMapper.ToCourseSessionDtoExpr,
            ct: ct
            );

        return courseSession is null
            ? Error.NotFound("CourseSession.NotFound", $"CourseSession with '{id}' was not found.")
            : courseSession;
    }
}