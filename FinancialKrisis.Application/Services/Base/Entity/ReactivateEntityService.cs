using FinancialKrisis.Application.Helpers;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public abstract class ReactivateEntityService<TEntity, TEntityRepository>(TEntityRepository pEntityRepository, IUnitOfWork pUnitOfWork)
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
            await pUnitOfWork.SaveChangesAsync();
        }
        catch (Exception pEx)
        {
            pUnitOfWork.DiscardChanges();
            throw ErrorMessageResolver.Resolve(pEx);
        }
    }
}
