using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Domain.Entities;

public abstract class ActivatableEntity : Entity, IActivatable
{
    public bool IsActive { get; protected set; } = true;

    public DateTime DeactivatedAt { get; protected set; }

    public virtual void Deactivate()
    {
        IsActive = false;
        DeactivatedAt = DateTime.Now;
    }
}
