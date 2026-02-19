using ClassCloud.Application.Abstractions.Persistence;
using ClassCloud.Application.Common.Errors;
using ClassCloud.Application.Dtos.CourseSessions;
using ClassCloud.Application.Mappers;
using ClassCloud.Domain.Entities;


namespace ClassCloud.Application.Services;

public class CourseSessionService(ICourseSessionRepository courseSessionRepository)
{
    private readonly ICourseSessionRepository _courseSessionRepository = courseSessionRepository;

    // Create CourseSession
    public async Task<ErrorOr<CourseSessionDto>> CreateCourseSessionAsync(CreateCourseSessionDto dto, CancellationToken ct = default)
    {
        var exists = await _courseSessionRepository.ExistsAsync(x => x.StartDate == dto.StartDate);
        if (exists)
            return Error.Conflict("CourseSession.Conflict", $"CourseSession with '{dto.StartDate}' already exists.");

        var savedCourseSession = await _courseSessionRepository.CreateAsync(new CourseSessionEntity { StartDate = dto.StartDate, EndDate = dto.EndDate, MaxParticipants = dto.MaxParticipants }, ct);
        return CourseSessionMapper.ToCourseSessionDto(savedCourseSession);
    }

    // Get one selected coursesession by Id.
    public async Task<ErrorOr<CourseSessionDto>> GetOneCourseSessionAsync(int id, CancellationToken ct = default)
    {
        var courseSession = await _courseSessionRepository.GetOneAsync(
            where: cs => cs.Id == id,
            select: CourseSessionMapper.ToCourseSessionDtoExpr,
            ct: ct
            );

        return courseSession is null
            ? Error.NotFound("CourseSession.NotFound", $"CourseSession with '{id}' was not found.")
            : courseSession;
    }
}