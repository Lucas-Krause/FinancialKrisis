using FinancialKrisis.Domain.ValueObjects;

namespace FinancialKrisis.Domain.Interfaces;

public interface INamedEntity : IEntity
{
    EntityName Name { get; }
}
