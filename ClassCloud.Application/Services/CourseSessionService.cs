using ClassCloud.Application.Abstractions.Persistence;
using ClassCloud.Application.Common.Errors;
using ClassCloud.Application.Dtos.CourseSessions;
using ClassCloud.Application.Dtos.Participants;
using ClassCloud.Application.Mappers;
using ClassCloud.Domain.Entities;

namespace ClassCloud.Application.Services;

public class CourseSessionService(ICourseSessionRepository courseSessionRepository)
{
    private readonly ICourseSessionRepository _courseSessionRepository = courseSessionRepository;

    //public async Task<ErrorOr<CourseSessionDto>> CreateCourseSessionAsync(CreateCourseSessionDto dto, CancellationToken ct = default)
    //{
    //    if (dto.EndDate <= dto.StartDate)
    //        return Error.Validation("CourseSession.InvalidDateRange", "EndDate must be after StartDate.");

    //    var CourseSessionEntity = new CourseSessionEntity{ StartDate = dto.StartDate, EndDate = dto.EndDate, MaxParticipants = dto.MaxParticipants, CourseId = dto.CourseId, LocationId = dto.LocationId };

    //    var savedCourseSession = await _courseSessionRepository.CreateAsync(new CourseSessionEntity { StartDate = dto.StartDate, EndDate = dto.EndDate, MaxParticipants = dto.MaxParticipants, CourseId = dto.CourseId, LocationId = dto.LocationId }, ct);
    //    return CourseSessionMapper.ToCourseSessionDto(savedCourseSession);
    //}

    //public async Task<ErrorOr<CourseSessionDto>> CreateCourseSessionAsync(CreateCourseSessionDto dto, CancellationToken ct = default)
    //{
    //    var exists = await _courseSessionRepository.ExistsAsync(x => x.Id == dto.Id);
    //    if (exists)
    //        return Error.Conflict("CourseSession.Conflict", $"CourseSession with '{dto.Id}' already exists.");

    //    var savedCourseSession = await _courseSessionRepository.CreateAsync(new CourseSessionEntity { StartDate = dto.StartDate, EndDate = dto.EndDate, MaxParticipants = dto.MaxParticipants, CourseId = dto.CourseId, LocationId = dto.LocationId }, ct);
    //    return CourseSessionMapper.ToCourseSessionDto(savedCourseSession);
    

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