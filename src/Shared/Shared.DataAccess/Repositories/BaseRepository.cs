using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Shared.DataAccess.Common;

namespace Shared.DataAccess.Repositories;


public class BaseRepository<TEntity, TContext>(TContext context) : IWriteRepository<TEntity>, IReadRespository<TEntity>
    where TEntity : Entity
    where TContext : DbContext
{
    private IQueryable<TEntity> _query => context.Set<TEntity>();

    public async Task<Paginate<TEntity>> GetPaginateAsync(Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, int pageIndex = 1,
        int pageSize = 10,
        bool enableTracking = false)
    {
        IQueryable<TEntity> query = _query;
        if (filter != null) query = query.Where(filter);
        if (include != null) query = include(query);
        if (!enableTracking) query = query.AsNoTracking();

        return new Paginate<TEntity>(query.ToList(), pageIndex, pageSize);
    }

    public async Task<IQueryable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, int pageIndex = 1, int pageSize = 10,
        bool enableTracking = false)
    {
        IQueryable<TEntity> query = _query;
        if (filter != null) query = query.Where(filter);
        if (include != null) query = include(query);
        if (!enableTracking) query = query.AsNoTracking();

        return query;
    }

    public async Task<TEntity> GetByIdAsync(Guid id,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool enableTracking = false)
    {
        IQueryable<TEntity> query = _query;
        if (include != null) query = include(query);
        if (!enableTracking) query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter)
    {
        return await _query.FirstOrDefaultAsync(filter);
    }

    public async Task<TEntity> AddEntityAsync(TEntity entity)
    {
        context.Entry(entity).State = EntityState.Added;
        return entity;
    }

    public async Task<TEntity> ModifyEntityAsync(TEntity entity)
    {
        context.Entry(entity).State = EntityState.Modified;
        return entity;
    }

    public async Task<TEntity> DeleteEntityAsync(TEntity entity)
    {
        context.Entry(entity).State = EntityState.Deleted;
        return entity;
    }
}