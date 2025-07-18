using UserPortfolioService.Domain.Entities;

namespace UserPortfolioService.Application.Extensions;

public static class DomainExtensions
{
    public static Domain.UserPortfolioAggregate.UserPortfolio EnsureNotNull(
        this Domain.UserPortfolioAggregate.UserPortfolio userPortfolio)
    {
        if (userPortfolio == null) throw new ArgumentNullException("UserPortfolioAggregate is null");

        return userPortfolio;
    }

    public static UserPortfolioEntity EnsureNotNull(this UserPortfolioEntity userPortfolioEntity)
    {
        if (userPortfolioEntity == null) throw new ArgumentNullException("UserPortfolioAggregate is null");
        return userPortfolioEntity;
    }
}