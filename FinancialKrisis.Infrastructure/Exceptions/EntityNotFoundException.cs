namespace FinancialKrisis.Infrastructure.Errors;

public class EntityNotFoundException(Type pEntityType, Guid pId) : Exception
{
    public Type EntityType { get; } = pEntityType;
    public Guid EntityIdValue { get; } = pId;
}