using FinancialKrisis.Application.Enums;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Common.Records;

namespace FinancialKrisis.Application.Metadata;

public class AccountFieldCatalog
{
    public static readonly Dictionary<FieldKey, FieldMetadata> Fields =
        new()
        {
            { Account.Fields.Name, new("Nome", GrammaticalGender.Masculine) },
            { Account.Fields.AccountNumber, new("Número", GrammaticalGender.Masculine) },
            { Account.Fields.InitialBalance, new("Saldo Inicial", GrammaticalGender.Masculine) },
        };
}
