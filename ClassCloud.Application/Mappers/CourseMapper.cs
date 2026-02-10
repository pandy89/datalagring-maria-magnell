using ClassCloud.Application.Dtos.Course;
using ClassCloud.Domain.Entities;

namespace ClassCloud.Application.Mappers;

public class CourseMapper
{
    public static CourseDto ToCourseDto(CourseEntity entity) => new()
    {
        CourseCode = entity.CourseCode,
        CourseName = entity.CourseName,
        CourseDescription = entity.CourseDescription,
        CreatedAtUtc = entity.CreatedAtUtc,
        UpdatedAtUtc = entity.UpdatedAtUtc,
        IsDeleted = entity.IsDeleted,
        RowVersion = entity.RowVersion,
        
    };
}