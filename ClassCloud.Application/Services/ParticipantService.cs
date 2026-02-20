using ClassCloud.Application.Abstractions.Persistence;
using ClassCloud.Application.Common.Errors;
using ClassCloud.Application.Common.Results;
using ClassCloud.Application.Dtos.Participants;
using ClassCloud.Application.Mappers;
using ClassCloud.Domain.Entities;

namespace ClassCloud.Application.Services;

public class ParticipantService(IParticipantRepository participantRepository)
{
    private readonly IParticipantRepository _participantRepository = participantRepository;

    // Create participant
    public async Task<ErrorOr<ParticipantDto>> CreateParticipantAsync(CreateParticipantDto dto, CancellationToken ct = default)
    {
        var exists = await _participantRepository.ExistsAsync(x => x.Email == dto.Email);
        if (exists)
            return Error.Conflict("Participant.Conflict", $"Participant with '{dto.Email}' already exists.");

        var savedParticipant = await _participantRepository.CreateAsync(new ParticipantEntity { Email = dto.Email, FirstName = dto.FirstName, LastName = dto.LastName, PhoneNumber = dto.PhoneNumber }, ct);
        return ParticipantMapper.ToParticipantDto(savedParticipant);
    }

    // Get one participant with email
    public async Task<ErrorOr<ParticipantDto>> GetOneParticipantAsync(string email, CancellationToken ct = default)
    {
        var participant = await _participantRepository.GetOneAsync(x => x.Email == email, ct);
        return participant is not null
            ? ParticipantMapper.ToParticipantDto(participant)
            : Error.NotFound("Participant.NotFound", $"Participant with '{email}' was not found.");
    }

    // Get all order by email
    public async Task<IReadOnlyList<ParticipantDto>> GetAllParticipantsAsync(CancellationToken ct = default)
    {
        return await _participantRepository.GetAllAsync(
            select: p => new ParticipantDto(p.Email, p.FirstName, p.LastName, p.PhoneNumber, p.RowVersion),
            orderBy: o => o.OrderByDescending(x => x.Email),
            ct: ct
            );
    }

    // Update participant
    public async Task<ErrorOr<ParticipantDto>> UpdateParticipantAsync(string email, UpdateParticipantDto dto, CancellationToken ct = default)
    {
        var participant = await _participantRepository.GetOneAsync(x => x.Email == email, ct);
        if (participant is null)
            return Error.NotFound("Participant.NotFound", $"Participant with '{email}' was not found.");

        if (!participant.RowVersion.SequenceEqual(dto.RowVersion))
            return Error.Conflict("Participant.Conflict", "Updated by another user. Please try again.");

        participant.Email = dto.Email;
        participant.FirstName = dto.FirstName;
        participant.LastName = dto.LastName;
        participant.PhoneNumber = dto.PhoneNumber;
        participant.UpdatedAtUtc = DateTime.UtcNow;        
        
        await _participantRepository.SaveChangesAsync(ct);
        return ParticipantMapper.ToParticipantDto(participant);
    }

    // Delete participant by email
    public async Task<ErrorOr<Deleted>> DeleteParticipantAsync(string Email, CancellationToken ct = default)
    {
        var participant = await _participantRepository.GetOneAsync(x => x.Email == Email, ct);
        if (participant is null)
            return Error.NotFound("Participant.NotFound", $"Participant with '{Email}' was not found.");

        await _participantRepository.DeleteAsync(participant, ct);
        return Result.Deleted;
    }
}
