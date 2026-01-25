using FinancialKrisis.Application.Enums;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Common.Records;

namespace FinancialKrisis.Application.Metadata;

public class SubcategoryFieldCatalog
{
    public static readonly Dictionary<FieldKey, FieldMetadata> Fields =
        new()
        {
            { Subcategory.Fields.Name, new("Nome", GrammaticalGender.Masculine) },
            { Subcategory.Fields.Category, new("Categoria", GrammaticalGender.Feminine) },
        };
}
