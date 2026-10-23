using FinancialKrisis.Application.Helpers;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public abstract class DeleteEntityService<TEntity, TEntityRepository>(TEntityRepository pEntityRepository, IUnitOfWork pUnitOfWork)
    where TEntity : IEntity, IDeletable
    where TEntityRepository : IGenericRepository<TEntity>, IDeletableRepository<TEntity>
{
    public async Task ExecuteAsync(Guid pEntityId)
    {
        try
        {
            TEntity entity = await pEntityRepository.GetByIdOrThrowAsync(pEntityId);
            pEntityRepository.Remove(entity);
            await pUnitOfWork.SaveChangesAsync();
        }
        catch (Exception pEx)
        {
            pUnitOfWork.DiscardChanges();
            throw ErrorMessageResolver.Resolve(pEx);
        }
    }
}
