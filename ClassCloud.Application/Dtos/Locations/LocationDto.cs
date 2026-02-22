namespace ClassCloud.Application.Dtos.Locations;

public record LocationDto
(
    int Id,
    string Name,
    DateTime CreatedAtUtc,
    DateTime UpdatedAtUtc,
    byte[] RowVersion
);