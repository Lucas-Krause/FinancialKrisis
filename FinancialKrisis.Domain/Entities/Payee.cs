using FinancialKrisis.Common.Records;
using FinancialKrisis.Domain.Abstractions;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Domain.Exceptions;

namespace FinancialKrisis.Domain.Entities;

public class Payee : IEntity, IActivatable
{
    public static class Fields
    {
        public static readonly FieldKey Name = new("Name");
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public bool IsActive { get; private set; }

    public List<Transaction> Transactions { get; private set; } = [];

    private Payee() { }

    public Payee(string pName)
    {
        ValidateName(pName);

        Id = Guid.NewGuid();
        Name = pName;
        IsActive = true;
    }

    public void ChangeName(string pNewName)
    {
        ValidateName(pNewName);
        Name = pNewName;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    private static void ValidateName(string pName)
    {
        if (string.IsNullOrWhiteSpace(pName))
            throw new DomainRuleException(DomainRuleErrorCode.RequiredField, typeof(Payee), Fields.Name);
    }
}
