namespace FinancialKrisis.Domain.Interfaces;

public interface IGenericRepository<TEntity>
    where TEntity : IEntity
{
    Task AddAsync(TEntity pEntity);
    Task UpdateAsync(TEntity pEntity);
    Task<TEntity?> GetByIdAsync(Guid pId);
    Task<TEntity> GetByIdOrThrowAsync(Guid pId);
    Task<IReadOnlyList<TEntity>> GetAllAsync();
}
