namespace FinancialKrisis.Domain.Interfaces;

public interface IDeletableRepository<TEntity>
    where TEntity : IEntity, IDeletable
{
    void Remove(TEntity pEntity);
}
