using FinancialKrisis.Common.Records;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Domain.Exceptions;

namespace FinancialKrisis.Domain.Entities;

public class Account : IEntity, IActivatable
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
        ValidateName(pName);
        ValidateAccountNumber(pAccountNumber);
        ValidateInitialBalance(pInitialBalance);

        Id = Guid.NewGuid();
        Name = pName;
        AccountNumber = pAccountNumber;
        InitialBalance = pInitialBalance;
        IsActive = true;
    }

    public void ChangeName(string pNewName)
    {
        ValidateName(pNewName);
        Name = pNewName;
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

    public void Deactivate()
    {
        IsActive = false;
    }

    private static void ValidateName(string pName)
    {
        if (string.IsNullOrWhiteSpace(pName))
            throw new DomainRuleException(DomainRuleErrorCode.RequiredField, typeof(Account), Fields.Name);
    }

    private static void ValidateAccountNumber(string pAccountNumber)
    {
        if (string.IsNullOrWhiteSpace(pAccountNumber))
            throw new DomainRuleException(DomainRuleErrorCode.RequiredField, typeof(Account), Fields.AccountNumber);
    }
    private static void ValidateInitialBalance(decimal pInitialBalance)
    {
        if (pInitialBalance < 0)
            throw new DomainRuleException(DomainRuleErrorCode.NegativeValue, typeof(Account), Fields.InitialBalance);
    }
}
