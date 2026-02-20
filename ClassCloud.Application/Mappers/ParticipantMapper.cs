using ClassCloud.Application.Dtos.Participants;
using ClassCloud.Domain.Entities;

namespace ClassCloud.Application.Mappers;

public class ParticipantMapper
{
    public static ParticipantDto ToParticipantDto(ParticipantEntity entity) => new
    (
        entity.Email,
        entity.FirstName,
        entity.LastName,
        entity.PhoneNumber,
        entity.RowVersion
    );
}
