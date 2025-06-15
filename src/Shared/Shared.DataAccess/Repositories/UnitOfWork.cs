using Microsoft.EntityFrameworkCore;

namespace Shared.DataAccess.Repositories;

public class UnitOfWork<TContext>(TContext context):IUnitOfWork
where TContext : DbContext
{
    public Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
       return  context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        context.Dispose();
    }
}