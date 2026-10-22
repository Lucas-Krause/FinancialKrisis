namespace FinancialKrisis.Domain.Entities;

public partial class Account
{
    public void ChangeName(string pNewName)
    {
        ValidateName(pNewName);
        Name = pNewName.Trim();
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
