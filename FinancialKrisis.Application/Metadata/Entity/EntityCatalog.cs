using FinancialKrisis.Application.Enums;
using FinancialKrisis.Domain.Entities;

namespace FinancialKrisis.Application.Metadata;

public static class EntityCatalog
{
    public static readonly Dictionary<Type, EntityMetadata> Entities =
        new()
        {
            { typeof(Account), new("Conta", GrammaticalGender.Feminine) },
            { typeof(Category), new("Categoria", GrammaticalGender.Feminine) },
            { typeof(Subcategory), new("Subcategoria", GrammaticalGender.Feminine) },
            { typeof(Payee), new("Beneficiário", GrammaticalGender.Masculine) },
            { typeof(Transaction), new("Transação", GrammaticalGender.Feminine) },
            { typeof(PlannedTransaction), new("Transação planejada", GrammaticalGender.Feminine) },
        };
}