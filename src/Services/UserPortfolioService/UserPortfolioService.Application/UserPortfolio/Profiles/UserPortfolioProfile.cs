using AutoMapper;
using UserPortfolioService.Domain.Entities;
using UserPortfolioService.UserPortfolio.Commands.Command;
using UserPortfolioService.UserPortfolio.Commands.Handler;
using UserPortfolioService.UserPortfolio.Dtos;

namespace UserPortfolioService.Application.UserPortfolio.Profiles;

public class UserPortfolioProfile : Profile
{
    public UserPortfolioProfile()
    {
        // Dto to entity and reverse
        CreateMap<TelegramEntity, TelegramDto>().ReverseMap();
        CreateMap<CryptoWalletEntity, CryptoWalletDto>().ReverseMap();
        CreateMap<TwitterEntity, TwitterDto>().ReverseMap();
        CreateMap<UserPortfolioEntity, UserPortfolioDto>().ReverseMap();
        CreateMap<UserPortfolioEntity, AddUserPortfolioCommand>().ReverseMap();
    }
}