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
            new CourseDto(
                cs.Course.CourseCode,
                cs.Course.CourseName,
                cs.Course.CourseDescription,
                cs.Course.CreatedAtUtc,
                cs.Course.UpdatedAtUtc,
                cs.Course.IsDeleted,
                cs.Course.RowVersion
            ),
            new LocationDto(
                cs.Location.Name
            )
        );

    public static CourseSessionDto ToCourseSessionDto(CourseSessionEntity entity) => new
    (
        entity.Id,
        entity.StartDate,
        entity.EndDate,
        entity.MaxParticipants,
        entity.CreatedAtUtc,
        entity.UpdatedAtUtc,
        new CourseDto
        (
            entity.Course.CourseCode,
            entity.Course.CourseName,
            entity.Course.CourseDescription,
            entity.Course.CreatedAtUtc,
            entity.Course.UpdatedAtUtc,
            entity.Course.IsDeleted,
            entity.Course.RowVersion
        ),
        new LocationDto(entity.Location.Name)
    );
}
