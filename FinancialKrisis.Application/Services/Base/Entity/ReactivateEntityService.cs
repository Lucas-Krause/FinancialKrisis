using FinancialKrisis.Application.Helpers;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public abstract class ReactivateEntityService<TEntity, TEntityRepository>(TEntityRepository pEntityRepository)
    where TEntity : IEntity, IActivatable
    where TEntityRepository : IGenericRepository<TEntity>
{
    protected virtual Task ValidateReactivation(TEntity pEntity)
    {
        return Task.CompletedTask;
    }

    public async Task ExecuteAsync(Guid pEntityId)
    {
        try
        {
            TEntity entity = await pEntityRepository.GetByIdOrThrowAsync(pEntityId);
            await ValidateReactivation(entity);
            entity.Reactivate();
            await pEntityRepository.UpdateAsync(entity);
        }
        catch (Exception pEx)
        {
            throw ErrorMessageResolver.Resolve(pEx);
        }
    }
}
