using FinancialKrisis.Application.Helpers;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public abstract class DeactivateEntityService<TEntity, TEntityRepository>(TEntityRepository pEntityRepository)
    where TEntity : IEntity, IActivatable
    where TEntityRepository : IGenericRepository<TEntity>
{
    public async Task ExecuteAsync(Guid pEntityId)
    {
        try
        {
            TEntity entity = await pEntityRepository.GetByIdOrThrowAsync(pEntityId);
            entity.Deactivate();
            await pEntityRepository.UpdateAsync(entity);
        }
        catch (Exception pEx)
        {
            throw ErrorMessageResolver.Resolve(pEx);
        }
    }
}
