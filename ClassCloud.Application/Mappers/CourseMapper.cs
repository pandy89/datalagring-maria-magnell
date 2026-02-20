using ClassCloud.Application.Dtos.Course;
using ClassCloud.Domain.Entities;

namespace ClassCloud.Application.Mappers;

public class CourseMapper
{
    public static CourseDto ToCourseDto(CourseEntity entity) => new
    (
        entity.Id,
        entity.CourseCode,
        entity.CourseName,
        entity.CourseDescription,
        entity.CreatedAtUtc,
        entity.UpdatedAtUtc,
        entity.IsDeleted,
        entity.RowVersion
    );
}