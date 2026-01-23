using FinancialKrisis.Application.Enums;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Identity;

namespace FinancialKrisis.Application.Metadata;

public class CategoryFieldCatalog
{
    public static readonly Dictionary<FieldKey, FieldMetadata> Fields =
        new()
        {
            { Category.Fields.Name, new("Nome", GrammaticalGender.Masculine) },
        };
}
