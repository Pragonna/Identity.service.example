using System.Text.Json;

namespace UserPortfolioService.Infrastructure.External;

public interface ICryptoPriceServer
{
    Task<Dictionary<string, decimal>> GetPricesAsync();
}

public class CryptoPriceService : ICryptoPriceServer
{
    public async Task<Dictionary<string, decimal>> GetPricesAsync()
    {
        using var client = new HttpClient();
        var url = "https://api.coingecko.com/api/v3/simple/price?ids=bitcoin,ethereum,tether&vs_currencies=usd";
        var response = await client.GetAsync(url);
        var json = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, decimal>>>(json);

        return data.ToDictionary(
            x => x.Key.ToUpper(), // e.g., BITCOIN
            x => x.Value["usd"]
        );
    }
}