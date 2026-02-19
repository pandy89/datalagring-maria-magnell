using ClassCloud.Application.Abstractions.Persistence;
using ClassCloud.Application.Common.Errors;
using ClassCloud.Application.Common.Results;
using ClassCloud.Application.Dtos.Participants;
using ClassCloud.Application.Dtos.Teachers;
using ClassCloud.Application.Mappers;
using ClassCloud.Domain.Entities;

namespace ClassCloud.Application.Services;

public class TeacherService(ITeacherRepository teacherRepository)
{
    private readonly ITeacherRepository _teacherRepository = teacherRepository;

    // Create teacher
    public async Task<ErrorOr<TeacherDto>> CreateTeacherAsync(CreateTeacherDto dto, CancellationToken ct = default)
    {
        var exists = await _teacherRepository.ExistsAsync(x => x.Email == dto.Email);
        if (exists)
            return Error.Conflict("Teacher.Conflict", $"Teacher with '{dto.Email}' already exists.");

        var savedTeacher = await _teacherRepository.CreateAsync(new TeacherEntity { Email = dto.Email, FirstName = dto.FirstName, LastName = dto.LastName, Expertise = dto.Expertise }, ct);
        return TeacherMapper.ToTeacherDto(savedTeacher);
    }

    // Get one teacher with email
    public async Task<ErrorOr<TeacherDto>> GetOneTeacherAsync(string email, CancellationToken ct = default)
    {
        var teacher = await _teacherRepository.GetOneAsync(x => x.Email == email, ct);
        return teacher is not null
            ? TeacherMapper.ToTeacherDto(teacher)
            : Error.NotFound("Teacher.NotFound", $"Teacher with '{email}' was not found.");
    }

    // Get all order by email
    public async Task<IReadOnlyList<TeacherDto>> GetAllTeachersAsync(CancellationToken ct = default)
    {
        return await _teacherRepository.GetAllAsync(
            select: t => new TeacherDto(t.Email, t.FirstName, t.LastName, t.Expertise , t.CreatedAtUtc, t.UpdatedAtUtc, t.IsDeleted, t.RowVersion ),
            orderBy: o => o.OrderByDescending(x => x.Email),
            ct: ct
            );
    }

    // Update teacher
    public async Task<ErrorOr<TeacherDto>> UpdateTeacherAsync(string Email, UpdateTeacherDto dto, CancellationToken ct = default)
    {
        var teacher = await _teacherRepository.GetOneAsync(x => x.Email == Email, ct);
        if (teacher is null)
            return Error.NotFound("Teacher.NotFound", $"Teacher with '{Email}' was not found.");

        if (!teacher.RowVersion.SequenceEqual(dto.RowVersion))
            return Error.Conflict("Teacher.Conflict", "Updated by another user. Please try again.");

        teacher.Email = dto.Email;
        teacher.FirstName = dto.FirstName;
        teacher.LastName = dto.LastName;
        teacher.Expertise = dto.Expertise;
        teacher.UpdatedAtUtc = DateTime.UtcNow;

        await _teacherRepository.SaveChangesAsync(ct);
        return TeacherMapper.ToTeacherDto(teacher);
    }

    // Delete teacher
    public async Task<ErrorOr<Deleted>> DeleteTeacherAsync(string Email, CancellationToken ct = default)
    {
        var teacher = await _teacherRepository.GetOneAsync(x => x.Email == Email, ct);
        if (teacher is null)
            return Error.NotFound("Teacher.NotFound", $"Teacher with '{Email}' was not found.");

        await _teacherRepository.DeleteAsync(teacher, ct);
        return Result.Deleted;
    }
}
