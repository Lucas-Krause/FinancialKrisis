using FinancialKrisis.Application.Enums;
using FinancialKrisis.Common.Records;
using FinancialKrisis.Domain.Entities;

namespace FinancialKrisis.Application.Metadata;

public class PayeeFieldCatalog
{
    public static readonly Dictionary<FieldKey, GrammarMetadata> Fields = new()
    {
        { Payee.Fields.Name, new("Nome", GrammaticalGender.Masculine) },
    };
}
