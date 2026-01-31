using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Helpers;
using FinancialKrisis.Domain.Abstractions;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public abstract class UpdateEntityService<TEntity, TEntityRepository, TUpdateEntityDTO>(TEntityRepository pEntityRepository)
    where TEntity : IEntity
    where TUpdateEntityDTO : IUpdateDTO
    where TEntityRepository : IGenericRepository<TEntity>
{
    protected abstract Task ApplyChangesToEntity(TEntity pEntity, TUpdateEntityDTO pUpdateDTO);

    public async Task<TEntity> ExecuteAsync(TUpdateEntityDTO pUpdateDTO)
    {
        try
        {
            TEntity entity = await pEntityRepository.GetByIdOrThrowAsync(pUpdateDTO.Id);

            if (entity is IActivatable activatable)
                ActiveEntityValidator.EnsureIsActive(activatable);

            await ApplyChangesToEntity(entity, pUpdateDTO);
            await pEntityRepository.UpdateAsync(entity);
            return entity;
        }
        catch (Exception pEx)
        {
            throw ErrorMessageResolver.Resolve(pEx);
        }
    }
}
