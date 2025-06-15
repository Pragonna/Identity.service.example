using Microsoft.EntityFrameworkCore;

namespace Shared.DataAccess.Repositories;

public interface IUnitOfWork : IDisposable
{
    Task<int> CommitAsync(CancellationToken cancellationToken = default);
}