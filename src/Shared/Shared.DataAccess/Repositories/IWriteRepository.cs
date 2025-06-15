using Shared.DataAccess.Common;

namespace Shared.DataAccess.Repositories;

public interface IWriteRepository<TEntity> where TEntity : Entity
{
    Task<TEntity> AddEntityAsync(TEntity entity);
    Task<TEntity> ModifyEntityAsync(TEntity entity);
    Task<TEntity> DeleteEntityAsync(TEntity entity);
}