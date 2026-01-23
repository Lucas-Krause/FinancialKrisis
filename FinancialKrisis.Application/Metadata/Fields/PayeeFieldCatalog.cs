using FinancialKrisis.Application.Enums;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Identity;

namespace FinancialKrisis.Application.Metadata;

public class PayeeFieldCatalog
{
    public static readonly Dictionary<FieldKey, FieldMetadata> Fields =
        new()
        {
            { Payee.Fields.Name, new("Nome", GrammaticalGender.Masculine) },
        };
}
