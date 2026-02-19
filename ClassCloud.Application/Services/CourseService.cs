using ClassCloud.Application.Abstractions.Persistence;
using ClassCloud.Application.Common.Errors;
using ClassCloud.Application.Common.Results;
using ClassCloud.Application.Dtos.Course;
using ClassCloud.Application.Mappers;
using ClassCloud.Domain.Entities;

namespace ClassCloud.Application.Services;

public class CourseService(ICourseRepository courseRepository)
{
    private readonly ICourseRepository _courseRepository = courseRepository;

    // Create
    public async Task<ErrorOr<CourseDto>> CreateCourseAsync(CreateCourseDto dto, CancellationToken ct = default)
    {
        var exisits = await _courseRepository.ExistsAsync(x => x.CourseCode == dto.CourseCode);
        if (exisits)
            return Error.Conflict("Course.Conflict", $"Course witch '{dto.CourseCode}' already exsits");
        
        var savedCourse = await _courseRepository.CreateAsync(new CourseEntity { CourseCode = dto.CourseCode, CourseName = dto.CourseName, CourseDescription = dto.CourseDescription }, ct);
        return CourseMapper.ToCourseDto(savedCourse);
    }

    // Get one
    public async Task<ErrorOr<CourseDto>> GetOneCourseAsync(string courseCode, CancellationToken ct = default)
    {
        var course = await _courseRepository.GetOneAsync(x => x.CourseCode == courseCode, ct);
        return course is not null
            ? CourseMapper.ToCourseDto(course)
            : Error.NotFound("Courses.NotFound", $"Course witch '{courseCode}' was not found.");
    }

    // Get all
    public async Task<IReadOnlyList<CourseDto>> GetAllCoursesAsync (CancellationToken ct = default)
    {
        return await _courseRepository.GetAllAsync(
            select: c => new CourseDto (c.CourseCode, c.CourseName, c.CourseDescription, c.CreatedAtUtc, c.UpdatedAtUtc, c.IsDeleted, c.RowVersion),            
            orderBy: o => o.OrderByDescending(x => x.CreatedAtUtc),            
            ct: ct
            );
    }

    // Update
    public async Task<ErrorOr<CourseDto>> UpdateCourseAsync(string courseCode, UpdateCourseDto dto, CancellationToken ct = default)
    {
        var course = await _courseRepository.GetOneAsync(x => x.CourseCode == courseCode, ct);
        if (course is null)
            return Error.NotFound("Courses.NotFound", $"Course with '{courseCode}' was not found.");

        if (!course.RowVersion.SequenceEqual(dto.RowVersion))
            return Error.Conflict("Courses.Conflict", "Updated by another user. Try again.");

        course.CourseName = dto.CourseName;
        course.CourseDescription = dto.CourseDescription;
        course.UpdatedAtUtc = DateTime.UtcNow;

        await _courseRepository.SaveChangesAsync(ct);
        return CourseMapper.ToCourseDto(course);
    }

    // Delete
    public async Task<ErrorOr<Deleted>> DeleteCourseAsync(string CourseCode, CancellationToken ct = default)
    {
        var course = await _courseRepository.GetOneAsync(x => x.CourseCode == CourseCode, ct);
        if (course is null)
            return Error.NotFound("Course.NotFound", $"Course with '{CourseCode}' was not found.");

        await _courseRepository.DeleteAsync(course, ct);
        return Result.Deleted;
    }
}
