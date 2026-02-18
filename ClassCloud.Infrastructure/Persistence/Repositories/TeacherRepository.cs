using ClassCloud.Application.Abstractions.Persistence;
using ClassCloud.Domain.Entities;

namespace ClassCloud.Infrastructure.Persistence.Repositories;

public class TeacherRepository(ClassCloudDbContext context) : BaseRepositoy<TeacherEntity>(context), ITeacherRepository
{
}
