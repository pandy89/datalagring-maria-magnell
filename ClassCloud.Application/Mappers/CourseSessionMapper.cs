using ClassCloud.Application.Dtos.Course;
using ClassCloud.Application.Dtos.CourseSessions;
using ClassCloud.Application.Dtos.Locations;
using ClassCloud.Domain.Entities;
using System.Linq.Expressions;

namespace ClassCloud.Application.Mappers;

public class CourseSessionMapper
{
    public static Expression<Func<CourseSessionEntity, CourseSessionDto>> ToCourseSessionDtoExpr =>
        cs => new CourseSessionDto(
            cs.Id,
            cs.StartDate,
            cs.EndDate,
            cs.MaxParticipants,
            cs.CreatedAtUtc,
            cs.UpdatedAtUtc,
            cs.IsDeleted,
            cs.IsCanceled,
            
            new CourseDto(
                cs.Course.Id,
                cs.Course.CourseCode,
                cs.Course.CourseName,
                cs.Course.CourseDescription,
                cs.Course.CreatedAtUtc,
                cs.Course.UpdatedAtUtc,
                cs.Course.IsDeleted,
                cs.Course.RowVersion
            ),
            new LocationDto(
                cs.Location.Id,
                cs.Location.Name,
                cs.Location.CreatedAtUtc,
                cs.Location.UpdatedAtUtc,
                cs.Location.RowVersion
            ),
            cs.RowVersion
        );

    public static CourseSessionDto ToCourseSessionDto(CourseSessionEntity entity) => new
    (
        entity.Id,
        entity.StartDate,
        entity.EndDate,
        entity.MaxParticipants,
        entity.CreatedAtUtc,
        entity.UpdatedAtUtc,
        entity.IsDeleted,
        entity.IsCanceled,        
        new CourseDto
        (
            entity.Course.Id,
            entity.Course.CourseCode,
            entity.Course.CourseName,
            entity.Course.CourseDescription,
            entity.Course.CreatedAtUtc,
            entity.Course.UpdatedAtUtc,
            entity.Course.IsDeleted,
            entity.Course.RowVersion
        ),
        new LocationDto(
            entity.Location.Id,
            entity.Location.Name,
            entity.Location.CreatedAtUtc,
            entity.Location.UpdatedAtUtc,
            entity.Location.RowVersion
            ),
        entity.RowVersion
    );
}
