using ClassCloud.Application.Abstractions.Persistence;
using ClassCloud.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ClassCloud.Infrastructure.Persistence.Repositories;

public class CourseSessionRepository(ClassCloudDbContext context) : BaseRepositoy<CourseSessionEntity>(context), ICourseSessionRepository
{
    public override async Task<CourseSessionEntity?> GetOneAsync(Expression<Func<CourseSessionEntity, bool>> where, CancellationToken ct = default)
    {
        return await _context.CourseSessions.Include(x => x.Course).Include(x => x.Location).FirstOrDefaultAsync(where, ct);
    }
}
