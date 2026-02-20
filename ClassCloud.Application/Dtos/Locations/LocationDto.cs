namespace ClassCloud.Application.Dtos.Locations;

public record LocationDto
(
    string Name,
    DateTime CreatedAtUtc,
    DateTime UpdatedAtUtc,
    byte[] RowVersion
);