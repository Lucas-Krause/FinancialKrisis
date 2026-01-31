using FinancialKrisis.Application.Helpers;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public abstract class GetEntityEntityByIdService<TEntity, TEntityRepository>(TEntityRepository pEntityRepository)
    where TEntity : IEntity
    where TEntityRepository : IGenericRepository<TEntity>
{
    public async Task<TEntity?> ExecuteAsync(Guid pEntityId)
    {
        try
        {
            return await pEntityRepository.GetByIdAsync(pEntityId);
        }
        catch (Exception pEx)
        {
            throw ErrorMessageResolver.Resolve(pEx);
        }
    }
}
