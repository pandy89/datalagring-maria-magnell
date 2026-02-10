using ClassCloud.Application.Abstractions.Persistence;
using ClassCloud.Domain.Entities;

namespace ClassCloud.Infrastructure.Persistence.Repositories;

public class CourseRepository(ClassCloudDbContext context) : BaseRepositoy<CourseEntity>(context), ICourseRepository
{
}
