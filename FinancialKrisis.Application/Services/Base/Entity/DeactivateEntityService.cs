using FinancialKrisis.Application.Helpers;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public abstract class DeactivateEntityService<TEntity, TEntityRepository>(TEntityRepository pEntityRepository, IUnitOfWork pUnitOfWork)
    where TEntity : IEntity, IActivatable
    where TEntityRepository : IGenericRepository<TEntity>
{
    public async Task ExecuteAsync(Guid pEntityId)
    {
        try
        {
            TEntity entity = await pEntityRepository.GetByIdOrThrowAsync(pEntityId);
            entity.Deactivate();
            await pUnitOfWork.SaveChangesAsync();
        }
        catch (Exception pEx)
        {
            pUnitOfWork.DiscardChanges();
            throw ErrorMessageResolver.Resolve(pEx);
        }
    }
}
