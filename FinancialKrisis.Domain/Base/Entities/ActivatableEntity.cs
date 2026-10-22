using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Domain.Exceptions;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Domain.Entities;

public abstract class ActivatableEntity : Entity, IActivatable
{
    public bool IsActive { get; protected set; } = true;

    public DateTime DeactivatedAt { get; protected set; }

    public virtual void Deactivate()
    {
        if (!IsActive)
            throw new DomainRuleException(DomainRuleErrorCode.EntityAlreadyInactive, GetType());

        IsActive = false;
        DeactivatedAt = DateTime.Now;
    }

    public virtual void Reactivate()
    {
        if (IsActive)
            throw new DomainRuleException(DomainRuleErrorCode.EntityAlreadyActive, GetType());

        IsActive = true;
        DeactivatedAt = default;
    }
}
