using FinancialKrisis.Application.Enums;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.ValueObjects;

namespace FinancialKrisis.Application.Metadata;

public static class TypeCatalog
{
    public static readonly Dictionary<Type, GrammarMetadata> Types = new()
    {
        { typeof(Account), new("Conta", GrammaticalGender.Feminine) },
        { typeof(Category), new("Categoria", GrammaticalGender.Feminine) },
        { typeof(Subcategory), new("Subcategoria", GrammaticalGender.Feminine) },
        { typeof(Payee), new("Beneficiário", GrammaticalGender.Masculine) },
        { typeof(Transaction), new("Transação", GrammaticalGender.Feminine) },
        { typeof(PlannedTransaction), new("Transação planejada", GrammaticalGender.Feminine) },
        { typeof(Schedule), new("Agendamento", GrammaticalGender.Masculine) },
    };
}