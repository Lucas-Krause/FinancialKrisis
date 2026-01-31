namespace FinancialKrisis.Domain.Interfaces;

public interface IDeletableRepository<TEntity>
    where TEntity : IEntity, IDeletable
{
    Task DeleteAsync(Guid pId);
}
