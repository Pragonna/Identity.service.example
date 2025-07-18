namespace UserPortfolioService.UserPortfolio.Dtos;

public class TwitterDto
{
    public string TwitterHandle { get; set; }
    public string? TwitterUserId { get; set; }
    public string? ProfileImageUrl { get; set; }
    public string? DisplayName { get; set; }
    public bool IsVerified { get; set; }
    public bool IsConnected { get; set; }
}