using FinancialKrisis.Application.Enums;
using FinancialKrisis.Application.Exceptions;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Helpers;

public static class ActiveEntityValidator
{
    public static IActivatable EnsureIsActive(IActivatable pEntity, Type? pOwnerType = null)
    {
        return !pEntity.IsActive
            ? throw new ApplicationRuleException(ApplicationRuleErrorCode.EntityInactive, pEntity.GetType(), null, pOwnerType)
            : pEntity;
    }
}
