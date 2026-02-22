using ClassCloud.Application.Abstractions.Persistence;
using ClassCloud.Domain.Entities;

namespace ClassCloud.Infrastructure.Persistence.Repositories;

public class CourseStatusRepository(ClassCloudDbContext context) : BaseRepositoy<CourseStatusEntity>(context), ICourseStatusRepository
{
}
