using FinancialKrisis.Common.Records;
using FinancialKrisis.Domain.ValueObjects;

namespace FinancialKrisis.Domain.Entities;

public partial class Account
{
    public Account() { }

    public static class Fields
    {
        public static readonly FieldKey Name = EntityName.Fields.Name;
        public static readonly FieldKey AccountNumber = new("AccountNumber");
        public static readonly FieldKey InitialBalance = new("InitialBalance");
    }

    public EntityName Name { get; private set; } = null!;
    public string AccountNumber { get; private set; } = null!;
    public decimal InitialBalance { get; private set; }

    public List<Transaction> Transactions { get; private set; } = [];
}
