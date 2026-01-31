using FinancialKrisis.Application.Helpers;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public abstract class DeleteEntityService<TEntity, TEntityRepository>(TEntityRepository pEntityRepository)
    where TEntity : IEntity, IDeletable
    where TEntityRepository : IDeletableRepository<TEntity>
{
    public async Task ExecuteAsync(Guid pEntityId)
    {
        try
        {
            await pEntityRepository.DeleteAsync(pEntityId);
        }
        catch (Exception pEx)
        {
            throw ErrorMessageResolver.Resolve(pEx);
        }
    }
}
