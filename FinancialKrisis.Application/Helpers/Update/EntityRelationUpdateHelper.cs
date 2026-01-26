using FinancialKrisis.Application.DTOs;

namespace FinancialKrisis.Application.Helpers;

public static class EntityRelationUpdateHelper
{
    public static bool ShouldAssign(Optional<Guid> pOptionalId)
    {
        return pOptionalId.IsDefined && pOptionalId.Value != Guid.Empty;
    }

    public static bool ShouldRemove(Optional<Guid> pOptionalId)
    {
        return pOptionalId.IsDefined && pOptionalId.Value == Guid.Empty;
    }
}
