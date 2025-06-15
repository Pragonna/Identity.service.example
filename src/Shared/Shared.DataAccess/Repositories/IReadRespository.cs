using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Shared.DataAccess.Common;

namespace Shared.DataAccess.Repositories;

public interface IReadRespository<TEntity> where TEntity : Entity
{
    Task<Paginate<TEntity>> GetPaginateAsync(
        Expression<Func<TEntity, bool>> filter=null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        int pageIndex = 1,
        int pageSize = 10,
        bool enableTracking = false);
    Task<IQueryable<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>> filter=null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        int pageIndex = 1,
        int pageSize = 10,
        bool enableTracking = false);

    Task<TEntity> GetByIdAsync(Guid id,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool enableTracking = false);

    Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter);
}