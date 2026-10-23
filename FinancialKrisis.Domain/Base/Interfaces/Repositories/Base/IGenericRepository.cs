namespace FinancialKrisis.Domain.Interfaces;

public interface IGenericRepository<TEntity>
    where TEntity : IEntity
{
    void Add(TEntity pEntity);
    Task<TEntity?> GetByIdAsync(Guid pId);
    Task<TEntity> GetByIdOrThrowAsync(Guid pId);
    Task<IReadOnlyList<TEntity>> GetAllAsync();
}
