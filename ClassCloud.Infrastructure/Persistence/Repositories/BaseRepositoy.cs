using ClassCloud.Application.Abstractions.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ClassCloud.Infrastructure.Persistence.Repositories;

public class BaseRepositoy<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly ClassCloudDbContext _context;
    private readonly DbSet<TEntity> _table;

    public BaseRepositoy(ClassCloudDbContext context)
    {
        _context = context; 
        _table = _context.Set<TEntity>();
    }

    // Create a new row in the table
    public virtual async Task<TEntity> CreateAsync(TEntity entity, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));
        _table.Add(entity);
        await _context.SaveChangesAsync(ct);
        return entity;
    }

    // Checks if there are any info in the table
    public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> findBy)
    {
        return await _table.AnyAsync(findBy);
    }

    // Fetch first row in table
    public virtual async Task<TEntity?> GetOneAsync(Expression<Func<TEntity, bool>> where, CancellationToken ct = default)
    {
        return await _table.Where(where).FirstOrDefaultAsync(ct);
    }

    // Get all by list and by select
    public virtual async Task<IReadOnlyList<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>>? where = null, 
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, 
        bool tracking = false, 
        CancellationToken ct = default, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = BuildQuery(tracking, includes);
        if (where is not null)
            query = query.Where(where);
        if (orderBy is not null)
            query = orderBy(query);
        return await query.ToListAsync(ct);
    }

    public virtual async Task<IReadOnlyList<TSelect>> GetAllAsync<TSelect>(
        Expression<Func<TEntity, TSelect>> select, 
        Expression<Func<TEntity, bool>>? where = null, 
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        bool tracking = false, CancellationToken ct = default, 
        params Expression<Func<TEntity, object>>[] includes)
    {
        var query = BuildQuery(tracking, includes);
        if(where is not null)
            query = query.Where(where);
        if(orderBy is not null)
            query = orderBy(query);
        return await query.Select(select).ToListAsync(ct); 
    }

    private IQueryable<TEntity> BuildQuery(bool tracking, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = tracking ? _table.AsTracking() : _table.AsNoTracking();

        foreach (var include in includes)
            query = query.Include(include);
        return query;
    }

    // Update row in table
    public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct = default)
    {
        _table.Update(entity);
        await _context.SaveChangesAsync(ct);
        return entity;
    }

    // Delete row in table
    public virtual async Task DeleteAsync(TEntity entity, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));
        _table.Remove(entity);
        await _context.SaveChangesAsync(ct);
    }

    // Save row i table
    public virtual async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return await _context.SaveChangesAsync(ct);
    }
}

    