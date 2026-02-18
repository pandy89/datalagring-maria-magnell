using ClassCloud.Application.Abstractions.Persistence;
using ClassCloud.Application.Common.Errors;
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

        var savedTeacher = await _teacherRepository.CreateAsync(new TeacherEntity { Email = dto.Email, FirstName = dto.FirstName, LastName = dto.LastName, Expertise = dto.Expertise,}, ct);
        return TeacherMapper.ToTeacherDto(savedTeacher);
    }

    // Get one teacher with email

    // Get all order by email

    // Update teacher

    // Delete teacher
}
