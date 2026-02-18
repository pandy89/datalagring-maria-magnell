using ClassCloud.Application.Dtos.Teachers;
using ClassCloud.Domain.Entities;

namespace ClassCloud.Application.Mappers;

public class TeacherMapper
{
    public static TeacherDto ToTeacherDto(TeacherEntity entity) => new
    (
        entity.FirstName,
        entity.LastName,
        entity.Expertise,
        entity.Email,
        entity.CreatedAtUtc,
        entity.UpdatedAtUtc,
        entity.IsDeleted,
        entity.RowVersion
    );
}
