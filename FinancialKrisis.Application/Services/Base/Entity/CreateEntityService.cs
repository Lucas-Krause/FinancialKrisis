using FinancialKrisis.Application.Helpers;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public abstract class CreateEntityService<TEntity, TEntityRepository, TCreateEntityDTO>(TEntityRepository pEntityRepository)
    where TEntity : IEntity
    where TEntityRepository : IGenericRepository<TEntity>
{
    protected abstract Task<TEntity> CreateEntity(TCreateEntityDTO pCreateDTO);

    public async Task<TEntity> ExecuteAsync(TCreateEntityDTO pCreateDTO)
    {
        try
        {
            TEntity entity = await CreateEntity(pCreateDTO);
            await pEntityRepository.AddAsync(entity);
            return entity;
        }
        catch (Exception pEx)
        {
            throw ErrorMessageResolver.Resolve(pEx);
        }
    }
}
