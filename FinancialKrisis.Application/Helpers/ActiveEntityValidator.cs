using FinancialKrisis.Domain.Common;

namespace FinancialKrisis.Application.Helpers;

public static class ActiveEntityValidator
{
    public static void EnsureIsActive(IActivatable pEntity, string pEntityName)
    {
        if (!pEntity.IsActive)
            throw new InvalidOperationException($"{pEntityName} is inactive.");
    }
}
