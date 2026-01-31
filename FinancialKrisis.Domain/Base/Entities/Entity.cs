using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Domain.Entities;

public abstract class Entity : IEntity
{
    public Guid Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; }
    public DateTime UpdatedAt { get; protected set; }

    public void SetCreatedAt(DateTime pCreatedAt)
    {
        CreatedAt = pCreatedAt;
    }

    public void SetUpdatedAt(DateTime pUpdatedAt)
    {
        UpdatedAt = pUpdatedAt;
    }
}
