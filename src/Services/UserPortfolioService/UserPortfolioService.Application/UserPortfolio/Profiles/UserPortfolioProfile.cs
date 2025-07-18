using AutoMapper;
using UserPortfolioService.Domain.Entities;
using UserPortfolioService.Domain.UserPortfolioAggregate;
using UserPortfolioService.UserPortfolio.Commands.Command;
using UserPortfolioService.UserPortfolio.Dtos;

namespace UserPortfolioService.Application.UserPortfolio.Profiles;

public class UserPortfolioProfile : Profile
{
    public UserPortfolioProfile()
    {
        // Aggregate to entity and reverse
        CreateMap<Domain.UserPortfolioAggregate.UserPortfolio, UserPortfolioEntity>().ReverseMap();
        CreateMap<TelegramEntity, TelegramEntity>().ReverseMap();
        CreateMap<CryptoWalletEntity, CryptoWallet>().ReverseMap();
        CreateMap<TwitterEntity, Twitter>().ReverseMap();

        CreateMap<CryptoWalletDto, CryptoWallet>().ReverseMap();
        CreateMap<AddUserPortfolioCommand, Domain.UserPortfolioAggregate.UserPortfolio>().ReverseMap();
        CreateMap<Domain.UserPortfolioAggregate.UserPortfolio, UserPortfolioDto>().ReverseMap();
        CreateMap<UserPortfolioEntity, UserPortfolioDto>().ReverseMap();
    }
}