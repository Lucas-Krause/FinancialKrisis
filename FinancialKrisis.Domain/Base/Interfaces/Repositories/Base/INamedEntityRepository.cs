using FinancialKrisis.Domain.ValueObjects;

namespace FinancialKrisis.Domain.Interfaces;

public interface INamedEntityRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : INamedEntity
{
    Task<bool> ExistsByNameAsync(EntityName pName, Guid? pIgnoredId = null);
}
