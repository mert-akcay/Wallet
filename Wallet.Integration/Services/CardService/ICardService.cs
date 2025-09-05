using Wallet.Integration.Models.CardService;

namespace Wallet.Integration.Services.CardService;

public interface ICardService
{
    public Task<CardServiceResponse?> GetNewCard(Guid walletId);
}
