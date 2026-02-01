using FinancialKrisis.Application.Enums;
using FinancialKrisis.Common.Records;
using FinancialKrisis.Domain.Entities;

namespace FinancialKrisis.Application.Metadata;

public class AccountFieldCatalog
{
    public static readonly Dictionary<FieldKey, GrammarMetadata> Fields = new()
    {
        { Account.Fields.Name, new("Nome", GrammaticalGender.Masculine) },
        { Account.Fields.AccountNumber, new("Número", GrammaticalGender.Masculine) },
        { Account.Fields.InitialBalance, new("Saldo Inicial", GrammaticalGender.Masculine) },
    };
}
