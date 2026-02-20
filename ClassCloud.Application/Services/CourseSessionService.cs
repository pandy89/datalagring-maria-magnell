using ClassCloud.Application.Abstractions.Persistence;
using ClassCloud.Application.Common.Errors;
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