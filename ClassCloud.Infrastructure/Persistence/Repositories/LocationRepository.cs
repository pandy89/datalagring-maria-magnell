using ClassCloud.Application.Abstractions.Persistence;
using ClassCloud.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClassCloud.Infrastructure.Persistence.Repositories;

public class LocationRepository(ClassCloudDbContext context) : BaseRepositoy<LocationEntity>(context), ILocationRepository
{
    
}
