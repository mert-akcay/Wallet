using Wallet.Integration.Helpers;
using Wallet.Integration.Models.CardService;

namespace Wallet.Integration.Services.CardService;

public class CardService(IHttpHelper httpHelper, HttpClient httpClient) : ICardService
{
    public async Task<CardServiceResponse?> GetNewCard(Guid walletId)
    {
        var request = httpHelper.PrepareHttpRequest(
            httpClient,
            HttpMethod.Post,
            "new-card",
            new { walletId }
            );
        return await httpHelper.SendAsync<CardServiceResponse>(request);
    }
}
