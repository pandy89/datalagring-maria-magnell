using ClassCloud.Application.Abstractions.Persistence;
using ClassCloud.Domain.Entities;

namespace ClassCloud.Infrastructure.Persistence.Repositories;

public class ParticipantRepository(ClassCloudDbContext context) : BaseRepositoy<ParticipantEntity>(context), IParticipantRepository
{
}
