namespace Wallet.Domain.Entities;

public class TransactionBankInfo : BaseEntity
{
    public string PayeeNameSurname { get; set; }

    public string PayeeIban { get; set; }

    public decimal BankCommissionAmount { get; set; }
    public string AdditionalInfo { get; set; }
}
