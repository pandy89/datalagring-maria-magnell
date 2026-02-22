using ClassCloud.Application.Abstractions.Persistence;
using ClassCloud.Application.Common.Errors;
using ClassCloud.Application.Common.Results;
using ClassCloud.Application.Dtos.CourseSessions;
using ClassCloud.Application.Mappers;
using ClassCloud.Domain.Entities;


namespace ClassCloud.Application.Services;

public class CourseSessionService(ICourseSessionRepository courseSessionRepository, ICourseRepository courseRepository, ILocationRepository locationRepository)
{
    private readonly ICourseSessionRepository _courseSessionRepository = courseSessionRepository;
    private readonly ICourseRepository _courseRepository = courseRepository;
    private readonly ILocationRepository _locationRepository = locationRepository;

    // Create CourseSession
    public async Task<ErrorOr<CourseSessionDto>> CreateCourseSessionAsync(CreateCourseSessionDto dto, CancellationToken ct = default)
    {
        var courseExists = await _courseRepository.ExistsAsync(x =>
            x.Id == dto.CourseId, ct);

        if (!courseExists)
            return Error.NotFound("Course.NotFound", "Course not found.");

        var locationExists = await _locationRepository.ExistsAsync(x => 
            x.Id == dto.LocationId, ct);

        if (!locationExists)
            return Error.NotFound("Location.NotFound", "Location not found.");
        
        var sessionExists = await _courseSessionRepository.ExistsAsync(x => 
            x.CourseId == dto.CourseId &&
            x.LocationId == dto.LocationId &&
            x.StartDate == dto.StartDate,
        ct);

        if (sessionExists)
            return Error.Conflict("CourseSession.Conflict", $"CourseSession already exists for this course, location and start date.");

        var entity = new CourseSessionEntity
        {
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            MaxParticipants = dto.MaxParticipants,
            CreatedAtUtc = dto.CreatedAt,
            UpdatedAtUtc = dto.UpdatedAt,
            CourseId = dto.CourseId,
            LocationId = dto.LocationId
        };

        var savedCourseSession = await _courseSessionRepository.CreateAsync(entity, ct);

        var courseSession = await GetOneCourseSessionAsync(savedCourseSession.Id, ct);
        if (courseSession == null)
        {
            return Error.NotFound("CourseSession.NotFound" ,"CourseSession not found.");
        }

        return courseSession;
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

    // Get all
    public async Task<IReadOnlyList<CourseSessionDto>> GetAllCourseSessionsAsync(CancellationToken ct = default)
    {
        return await _courseSessionRepository.GetAllAsync(
            select: CourseSessionMapper.ToCourseSessionDtoExpr,
            ct: ct
            );
    }

    // Update
    public async Task<ErrorOr<CourseSessionDto>> UpdateCourseSessionAsync(int id, UpdateCourseSessionDto dto, CancellationToken ct = default)
    {
        var courseSession = await _courseSessionRepository.GetOneAsync(x => x.Id == id, ct);
        if (courseSession is null)
            return Error.NotFound("CourseSession.NotFound", $"CourseSession with '{id}' was not found.");

        if (!courseSession.RowVersion.SequenceEqual(dto.RowVersion))
            return Error.Conflict("CourseSession.Conflict", "Updated by another user. Try again.");

        courseSession.StartDate = dto.StartDate;
        courseSession.EndDate = dto.EndDate;
        courseSession.MaxParticipants = dto.MaxParticipants;
        courseSession.LocationId = dto.LocationId;

        await _courseSessionRepository.SaveChangesAsync(ct);

        var courseSessionDto = await GetOneCourseSessionAsync(courseSession.Id, ct);
        if (courseSessionDto == null)
        {
            return Error.NotFound("CourseSession.NotFound", "CourseSession not found.");
        }

        return courseSessionDto;
    }

    // Delete
    public async Task<ErrorOr<Deleted>> DeleteCourseSessionAsync(int id, CancellationToken ct = default)
    {
        var courseSession = await _courseSessionRepository.GetOneAsync(x => x.Id == id, ct);
        if (courseSession is null)
            return Error.NotFound("CourseSession.NotFound", $"CourseSession with '{id}' was not found.");

        await _courseSessionRepository.DeleteAsync(courseSession, ct);
        return Result.Deleted;
    }
}