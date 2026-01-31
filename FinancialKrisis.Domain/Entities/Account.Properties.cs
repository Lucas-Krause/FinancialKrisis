using FinancialKrisis.Common.Records;

namespace FinancialKrisis.Domain.Entities;

public partial class Account
{
    public Account() { }

    public static class Fields
    {
        public static readonly FieldKey Name = new("Name");
        public static readonly FieldKey AccountNumber = new("AccountNumber");
        public static readonly FieldKey InitialBalance = new("InitialBalance");
    }

    public string Name { get; private set; } = null!;
    public string AccountNumber { get; private set; } = null!;
    public decimal InitialBalance { get; private set; }

    public List<Transaction> Transactions { get; private set; } = [];
}
