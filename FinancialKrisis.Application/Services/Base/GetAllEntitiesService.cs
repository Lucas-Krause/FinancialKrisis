using FinancialKrisis.Application.Helpers;
using FinancialKrisis.Domain.Abstractions;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public abstract class GetAllEntitiesService<TEntity, TEntityRepository>(TEntityRepository pEntityRepository)
    where TEntity : IEntity
    where TEntityRepository : IGenericRepository<TEntity>
{
    public async Task<IReadOnlyList<TEntity>> ExecuteAsync()
    {
        try
        {
            return await pEntityRepository.GetAllAsync();
        }
        catch (Exception pEx)
        {
            throw ErrorMessageResolver.Resolve(pEx);
        }
    }
}
