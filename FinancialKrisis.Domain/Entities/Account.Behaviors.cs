using FinancialKrisis.Domain.ValueObjects;

namespace FinancialKrisis.Domain.Entities;

public partial class Account
{
    public void ChangeName(string pNewName)
    {
        Name = new EntityName(pNewName, typeof(Account));
    }

    public void ChangeAccountNumber(string pNewAccountNumber)
    {
        ValidateAccountNumber(pNewAccountNumber);
        AccountNumber = pNewAccountNumber;
    }

    public void ChangeInitialBalance(decimal pNewInitialBalance)
    {
        ValidateInitialBalance(pNewInitialBalance);
        InitialBalance = pNewInitialBalance;
    }
}
