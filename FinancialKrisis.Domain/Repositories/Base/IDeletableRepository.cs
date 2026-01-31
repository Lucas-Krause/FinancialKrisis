using FinancialKrisis.Domain.Abstractions;

namespace FinancialKrisis.Domain.Repositories;

public interface IDeletableRepository<TEntity>
    where TEntity : IEntity, IDeletable
{
    Task DeleteAsync(Guid pId);
}
