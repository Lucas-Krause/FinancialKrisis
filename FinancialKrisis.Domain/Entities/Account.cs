namespace FinancialKrisis.Domain.Entities;

public partial class Account : ActivatableEntity
{
    public Account(string pName, string pAccountNumber, decimal pInitialBalance)
    {
        ValidateName(pName);
        ValidateAccountNumber(pAccountNumber);
        ValidateInitialBalance(pInitialBalance);

        Id = Guid.NewGuid();
        Name = pName;
        AccountNumber = pAccountNumber;
        InitialBalance = pInitialBalance;
    }
}
