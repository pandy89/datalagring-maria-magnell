using ClassCloud.Application.Abstractions.Persistence;
using ClassCloud.Application.Common.Errors;
using ClassCloud.Application.Common.Results;
using ClassCloud.Application.Dtos.CourseRegistrations;
using ClassCloud.Application.Dtos.CourseSessions;
using ClassCloud.Application.Dtos.Participants;
using ClassCloud.Application.Mappers;
using ClassCloud.Domain.Entities;

namespace ClassCloud.Application.Services;

public class CourseRegistrationService(ICourseRegistrationRepository courseRegistrationRepository, ICourseSessionRepository courseSessionRepository, ICourseStatusRepository courseStatusRepository, IParticipantRepository participantRepository)
{
    private readonly ICourseRegistrationRepository _courseRegistrationRepository = courseRegistrationRepository;
    private readonly ICourseSessionRepository _courseSessionRepository = courseSessionRepository;
    private readonly ICourseStatusRepository _courseStatusRepository = courseStatusRepository;
    private readonly IParticipantRepository _participantRepository = participantRepository; 

    // Create
    public async Task<ErrorOr<CourseRegistrationDto>> CreateCourseRegistrationAsync(CreateCourseRegistrationDto dto, CancellationToken ct = default)
    {
        var participantExists = await _participantRepository.ExistsAsync(
            x => x.Id == dto.ParticipantId, ct);
        if (!participantExists) 
            return Error.NotFound("Participant.NotFound", "Participant not found");

        var courseSessionExists = await _courseSessionRepository.ExistsAsync(
            x => x.Id == dto.CourseSessionId, ct);
        if (!courseSessionExists)
            return Error.NotFound("CourseSession.NotFound", "Course Session not found");

        var courseStatusExists = await _courseStatusRepository.ExistsAsync(
            x => x.Id == dto.CourseStatusId, ct);
        if (!courseStatusExists)
            return Error.NotFound("CourseStatus.NotFound", "Course Status not found");


        var courseRegistrationExists = await _courseRegistrationRepository.ExistsAsync(
            x => x.ParticipantId == dto.ParticipantId && x.CourseSessionId == dto.CourseSessionId,
            ct);
        if (courseRegistrationExists)
            return Error.Conflict("CourseRegistration.Conflict", "Participant is already registrated for this session.");


        var entity = new CourseRegistrationEntity
        {
            ParticipantId = dto.ParticipantId,
            CourseSessionId = dto.CourseSessionId,
            CourseStatusId = dto.CourseStatusId,
            RegistrationDate = dto.RegistrationDate,
            UpdatedAtUtc = dto.UpdatedAtUtc
        };

        var savedCourseRegistration = await _courseRegistrationRepository.CreateAsync(entity, ct);

        var courseRegistration = await GetOneCourseRegistrationAsync(savedCourseRegistration.Id, ct);
        if (courseRegistration is null)
            return Error.NotFound("CourseRegistration.NotFound", "Courseregistration not found,");

        return courseRegistration;
    }


    // Get one
    public async Task<ErrorOr<CourseRegistrationDto>> GetOneCourseRegistrationAsync(int id, CancellationToken ct = default)
    {
        var courseRegistration = await _courseRegistrationRepository.GetOneAsync(
            where: cs => cs.Id == id,
            select: CourseRegistrationMapper.ToCourseRegistrationDtoExpr,
            ct: ct
            );

        return courseRegistration is null
            ? Error.NotFound("CourseRegistration.NotFound", $"CourseRegistration with '{id}' was not found.")
            : courseRegistration;
    }

    // Get all
    public async Task<IReadOnlyList<CourseRegistrationDto>> GetAllCourseRegistrationsAsync(CancellationToken ct = default)
    {
        return await _courseRegistrationRepository.GetAllAsync(
            select: CourseRegistrationMapper.ToCourseRegistrationDtoExpr,
            ct: ct
            );
    }

    // Update
    public async Task<ErrorOr<CourseRegistrationDto>> UpdateCourseRegistrationAsync(int id, UpdateCourseRegistrationDto dto, CancellationToken ct = default)
    {
        var courseRegistration = await _courseRegistrationRepository.GetOneAsync(x => x.Id == id, ct);
        if (courseRegistration is null)
            return Error.NotFound("CourseRegistration.NotFound", $"CourseRegistration with '{id}' was not found.");

        if (!courseRegistration.RowVersion.SequenceEqual(dto.RowVersion))
            return Error.Conflict("CourseRegistration.Conflict", "Updated by another user. Try again.");

        courseRegistration.ParticipantId = dto.ParticipantId;
        courseRegistration.CourseSessionId = dto.CourseSessionId;
        courseRegistration.CourseStatusId = dto.CourseStatusId;

        await _courseRegistrationRepository.SaveChangesAsync(ct);

        var courseRegistrationDto = await GetOneCourseRegistrationAsync(courseRegistration.Id, ct);
        if (courseRegistrationDto == null)
        {
            return Error.NotFound("CourseRegistration.NotFound", "CourseRegistration not found.");
        }

        return courseRegistrationDto;
    }

    // Delete
    public async Task<ErrorOr<Deleted>> DeleteCourseRegistrationAsync(int id, CancellationToken ct = default)
    {
        var courseRegistration = await _courseRegistrationRepository.GetOneAsync(x => x.Id == id, ct);
        if (courseRegistration is null)
            return Error.NotFound("CourseRegistration.NotFound", $"CourseRegistration with '{id}' was not found.");

        await _courseRegistrationRepository.DeleteAsync(courseRegistration, ct);
        return Result.Deleted;
    }
}
