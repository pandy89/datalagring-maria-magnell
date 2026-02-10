using System.Linq.Expressions;

namespace ClassCloud.Application.Abstractions.Persistence;

public interface IBaseRepository<TEntity> where TEntity : class
{
    //Create
    Task<TEntity> CreateAsync(TEntity entity, CancellationToken ct = default);

    // Exisits
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> findBy);

    // Get One
    Task<TEntity?> GetOneAsync(Expression<Func<TEntity,bool>> where, CancellationToken ct = default);

    // Get whole entity, good for write/update, Less flexible
    Task<IReadOnlyList<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>>? where = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        bool tracking = false,
        CancellationToken ct = default,
        params Expression<Func<TEntity, object>>[] includes);

    // Get specific entites information, only selected fields, Perfect for reading, More flexible & efficient
    Task<IReadOnlyList<TSelect>> GetAllAsync<TSelect>(
        Expression<Func<TEntity, TSelect>> select,
        Expression<Func<TEntity, bool>>? where = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        bool tracking = false,
        CancellationToken ct = default,
        params Expression<Func<TEntity, object>>[] includes);

    // Update
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct = default);

    // Delete
    Task DeleteAsync(TEntity entity, CancellationToken ct = default);

    // Save
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}