using ClassCloud.Application.Dtos.Course;
using ClassCloud.Application.Dtos.CourseRegistrations;
using ClassCloud.Application.Dtos.CourseSessions;
using ClassCloud.Application.Dtos.Locations;
using ClassCloud.Application.Dtos.Participants;
using ClassCloud.Application.Dtos.CourseStatus;
using ClassCloud.Domain.Entities;
using System.Linq.Expressions;

namespace ClassCloud.Application.Mappers;

public class CourseRegistrationMapper
{
    public static Expression<Func<CourseRegistrationEntity, CourseRegistrationDto>> ToCourseRegistrationDtoExpr =>
        cs => new CourseRegistrationDto(
            cs.Id,
            cs.RegistrationDate,
            cs.UpdatedAtUtc,
            cs.IsDeleted,

            new ParticipantDto(
                cs.Participant.Id,
                cs.Participant.Email,
                cs.Participant.FirstName,
                cs.Participant.LastName,
                cs.Participant.PhoneNumber,
                cs.Participant.RowVersion
                ),

                new CourseSessionDto(
                    cs.CourseSession.Id,
                    cs.CourseSession.StartDate,
                    cs.CourseSession.EndDate,
                    cs.CourseSession.MaxParticipants,
                    cs.CourseSession.CreatedAtUtc,
                    cs.CourseSession.UpdatedAtUtc,
                    cs.CourseSession.IsDeleted,
                    cs.CourseSession.IsCanceled,                

                    new CourseDto(
                        cs.CourseSession.Course.Id,
                        cs.CourseSession.Course.CourseCode,
                        cs.CourseSession.Course.CourseName,
                        cs.CourseSession.Course.CourseDescription,
                        cs.CourseSession.Course.CreatedAtUtc,
                        cs.CourseSession.Course.UpdatedAtUtc,
                        cs.CourseSession.Course.IsDeleted,
                        cs.CourseSession.Course.RowVersion
                        ),

                    new LocationDto(
                        cs.CourseSession.Location.Id,
                        cs.CourseSession.Location.Name,
                        cs.CourseSession.Location.CreatedAtUtc,
                        cs.CourseSession.Location.UpdatedAtUtc,
                        cs.CourseSession.Location.RowVersion
                        ),
                    cs.CourseSession.RowVersion
                ),

            new CourseStatusDto(
                cs.CourseStatus.Id,
                cs.CourseStatus.StatusType,
                cs.CourseStatus.RowVersion
            ),

            cs.RowVersion
    );

    public static CourseRegistrationDto ToCourseRegistrationDto(CourseRegistrationEntity entity) => new
    (
        entity.Id,
        entity.RegistrationDate,
        entity.UpdatedAtUtc,
        entity.IsDeleted,

        new ParticipantDto(
            entity.Participant.Id,
            entity.Participant.Email,
            entity.Participant.FirstName,
            entity.Participant.LastName,
            entity.Participant.PhoneNumber,
            entity.Participant.RowVersion
            ),

            new CourseSessionDto(
                entity.CourseSession.Id,
                entity.CourseSession.StartDate,
                entity.CourseSession.EndDate,
                entity.CourseSession.MaxParticipants,
                entity.CourseSession.CreatedAtUtc,
                entity.CourseSession.UpdatedAtUtc,
                entity.CourseSession.IsDeleted,
                entity.CourseSession.IsCanceled,       

                new CourseDto(
                    entity.CourseSession.Course.Id,
                    entity.CourseSession.Course.CourseCode,
                    entity.CourseSession.Course.CourseName,
                    entity.CourseSession.Course.CourseDescription,
                    entity.CourseSession.Course.CreatedAtUtc,
                    entity.CourseSession.Course.UpdatedAtUtc,
                    entity.CourseSession.Course.IsDeleted,
                    entity.CourseSession.Course.RowVersion
                    ),

                new LocationDto(
                    entity.CourseSession.Location.Id,
                    entity.CourseSession.Location.Name,
                    entity.CourseSession.Location.CreatedAtUtc,
                    entity.CourseSession.Location.UpdatedAtUtc,
                    entity.CourseSession.Location.RowVersion
                    ),

                entity.CourseSession.RowVersion
            ),

        new CourseStatusDto(
            entity.CourseStatus.Id,
            entity.CourseStatus.StatusType,
            entity.CourseStatus.RowVersion
            ),
        entity.RowVersion
    );
}