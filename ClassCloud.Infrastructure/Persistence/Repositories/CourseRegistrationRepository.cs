using ClassCloud.Application.Abstractions.Persistence;
using ClassCloud.Domain.Entities;

namespace ClassCloud.Infrastructure.Persistence.Repositories;

public class CourseRegistrationRepository(ClassCloudDbContext context) : BaseRepositoy<CourseRegistrationEntity>(context), ICourseRegistrationRepository
{
}


