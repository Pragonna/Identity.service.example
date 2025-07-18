namespace UserPortfolioService.UserPortfolio.Dtos;

public class TelegramDto
{
    public string TelegramHandle { get; set; }
    public long TelegramUserId { get; set; }
    public string? FullName { get; set; }
    public Guid UserPortfolioId { get; set; }
    public bool IsSubscribed { get; set; }
}