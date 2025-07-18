using Shared.DataAccess.Repositories;
using UserPortfolioService.Application.Repositories;
using UserPortfolioService.Infrastructure.Persistence.Context;

namespace UserPortfolioService.Infrastructure.Persistence.Repositories;

public class UserPortfolioUnitOfWork(UserPortfolioDbContext context)
    : UnitOfWork<UserPortfolioDbContext>(context), IUserPortfolioUnitOfWork
{
}