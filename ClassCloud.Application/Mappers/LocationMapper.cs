using ClassCloud.Application.Dtos.Locations;
using ClassCloud.Domain.Entities;

namespace ClassCloud.Application.Mappers;

public class LocationMapper
{
    public static LocationDto ToLocationDto(LocationEntity entity) => new
    (
        entity.Name,
        entity.CreatedAtUtc,
        entity.UpdatedAtUtc
    );
}
