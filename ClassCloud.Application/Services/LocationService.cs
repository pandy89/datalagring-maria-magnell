using ClassCloud.Application.Abstractions.Persistence;
using ClassCloud.Application.Common.Errors;
using ClassCloud.Application.Common.Results;
using ClassCloud.Application.Dtos.Locations;
using ClassCloud.Application.Mappers;
using ClassCloud.Domain.Entities;

namespace ClassCloud.Application.Services;

public class LocationService(ILocationRepository locationRepository)
{
    private readonly ILocationRepository _locationRepository = locationRepository;

    // Create location
    public async Task<ErrorOr<LocationDto>> CreateLocationAsync(CreateLocationDto dto, CancellationToken ct = default)
    {
        var exists = await _locationRepository.ExistsAsync(x => x.Name == dto.Name);
        if (exists)
            return Error.Conflict("Location.Conflict", $"Location with '{dto.Name}' already exists.");

        var savedLocation = await _locationRepository.CreateAsync(new LocationEntity { Name = dto.Name }, ct);
        return LocationMapper.ToLocationDto(savedLocation);
    }

    // Get one location
    public async Task<ErrorOr<LocationDto>> GetOneLocationAsync(string Name, CancellationToken ct = default)
    {
        var location = await _locationRepository.GetOneAsync(x => x.Name == Name, ct);
        return location is not null
            ? LocationMapper.ToLocationDto(location)
            : Error.NotFound("Location.NotFound", $"Location with '{Name}' was not found.");
    }

    // Get all location
    public async Task<IReadOnlyList<LocationDto>> GetAllLocationsAsync(CancellationToken ct = default)
    {
        return await _locationRepository.GetAllAsync(
            select: l => new LocationDto(l.Name, l.CreatedAtUtc, l.UpdatedAtUtc),
            orderBy: o => o.OrderBy(x => x.Name),
            ct: ct
            );
    }

    // Update location
    public async Task<ErrorOr<LocationDto>> UpdateLocationAsync(string Name, UpdateLocationDto dto, CancellationToken ct = default)
    {
        var location = await _locationRepository.GetOneAsync(x => x.Name == Name, ct);
        if (location is null)
            return Error.NotFound("Location.NotFound", $"Location with '{Name}' was not found.");

        if (!location.RowVersion.SequenceEqual(dto.RowVersion))
            return Error.Conflict("Location.Conflict", "Updated by another user. Please try again.");

        location.Name = dto.Name;
        location.CreatedAtUtc = DateTime.UtcNow;
        location.UpdatedAtUtc = DateTime.UtcNow; 

        await _locationRepository.SaveChangesAsync(ct);
        return LocationMapper.ToLocationDto(location);
    }

    // Delete location
    public async Task<ErrorOr<Deleted>> DeleteLocationAsync(string Name, CancellationToken ct = default)
    {
        var location = await _locationRepository.GetOneAsync(x => x.Name == Name, ct);
        if (location is null)
            return Error.NotFound("Location.NotFound", $"Location with '{Name}' was not found.");

        await _locationRepository.DeleteAsync(location, ct);
        return Result.Deleted;
    }
}
