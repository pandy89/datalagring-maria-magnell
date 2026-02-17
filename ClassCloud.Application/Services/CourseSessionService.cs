using ClassCloud.Application.Abstractions.Persistence;
using ClassCloud.Application.Common.Errors;
using ClassCloud.Application.Dtos.CourseSessions;
using ClassCloud.Application.Mappers;

namespace ClassCloud.Application.Services;

public class CourseSessionService(ICourseSessionRepository repository)
{
    // Get one selected course by Id.
    public async Task<ErrorOr<CourseSessionDto>> GetOneCourseSessionAsync(int id, CancellationToken ct = default)
    {
        var courseSession = await repository.GetOneAsync(
            where: cs => cs.Id == id,
            select: CourseSessionMapper.ToCourseSessionDtoExpr,
            ct: ct
            );

        return courseSession is null
            ? Error.NotFound("CourseSession.NotFound", $"CourseSession with '{id}' was not found.")
            : courseSession;
    }
}
