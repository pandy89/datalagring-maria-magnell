using ClassCloud.Application.Dtos.Locations;
using ClassCloud.Domain.Entities;

namespace ClassCloud.Application.Mappers;

public class LocationMapper
{
    public static LocationDto ToLocationDto(LocationEntity entity) => new
    (
        entity.Id,
        entity.Name,
        entity.CreatedAtUtc,
        entity.UpdatedAtUtc,
        entity.RowVersion
    );
}
