using System.Runtime.CompilerServices;
using AutoMapper;
using EventBus.EventBus.Base.Abstraction;
using FluentAssertions;
using IdentityService.Domain.Enums;
using Moq;
using Shared.DataAccess;
using Shared.DataAccess.Common;
using UserPortfolioService.Application.Repositories;
using UserPortfolioService.Domain.Entities;
using UserPortfolioService.Domain.Enums;
using UserPortfolioService.Domain.UserPortfolioAggregate;
using UserPortfolioService.UserPortfolio.Commands.Command;
using UserPortfolioService.UserPortfolio.Commands.Handler;
using UserPortfolioService.UserPortfolio.Dtos;
using UserPortfolioService.UserPortfolio.Rules;

namespace UserPortfolio.UnitTest;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void ShouldBeSuccessResultWhenAddCryptoWalletCommandHandler()
    {
        
    }
}