using FinancialKrisis.Domain.Common;
using FinancialKrisis.Domain.Identity;

namespace FinancialKrisis.Domain.Entities;

public class Account : IActivatable
{
    public static class Fields
    {
        public static readonly FieldKey Name = new("Name");
        public static readonly FieldKey AccountNumber = new("AccountNumber");
        public static readonly FieldKey InitialBalance = new("InitialBalance");
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string AccountNumber { get; private set; } = null!;
    public decimal InitialBalance { get; private set; }
    public bool IsActive { get; private set; }

    private Account() { }

    public Account(string pName, string pAccountNumber, decimal pInitialBalance)
    {
        if (string.IsNullOrWhiteSpace(pName))
            throw new ArgumentException("Account name is required.");
        if (string.IsNullOrWhiteSpace(pAccountNumber))
            throw new ArgumentException("Account number is required.");
        if (pInitialBalance < 0)
            throw new ArgumentException("Initial balance cannot be negative.");

        Id = Guid.NewGuid();
        Name = pName;
        AccountNumber = pAccountNumber;
        InitialBalance = pInitialBalance;
        IsActive = true;
    }

    public void Rename(string pNewName)
    {
        if (string.IsNullOrWhiteSpace(pNewName))
            throw new ArgumentException("Account name is required.");
        Name = pNewName;
    }

    public void ChangeAccountNumber(string pNewAccountNumber)
    {
        if (string.IsNullOrWhiteSpace(pNewAccountNumber))
            throw new ArgumentException("Account number is required.");
        AccountNumber = pNewAccountNumber;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}
