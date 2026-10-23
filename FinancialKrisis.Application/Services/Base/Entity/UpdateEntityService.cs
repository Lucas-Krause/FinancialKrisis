using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Helpers;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public abstract class UpdateEntityService<TEntity, TEntityRepository, TUpdateEntityDTO>(TEntityRepository pEntityRepository, IUnitOfWork pUnitOfWork)
    where TEntity : IEntity
    where TUpdateEntityDTO : IUpdateDTO
    where TEntityRepository : IGenericRepository<TEntity>
{
    protected TEntityRepository EntityRepository { get; } = pEntityRepository;

    protected abstract Task ApplyChangesToEntity(TEntity pEntity, TUpdateEntityDTO pUpdateDTO);

    public async Task<TEntity> ExecuteAsync(TUpdateEntityDTO pUpdateDTO)
    {
        try
        {
            TEntity entity = await EntityRepository.GetByIdOrThrowAsync(pUpdateDTO.Id);

            if (entity is IActivatable activatable)
                ActiveEntityValidator.EnsureIsActive(activatable);

            await ApplyChangesToEntity(entity, pUpdateDTO);
            await pUnitOfWork.SaveChangesAsync();
            return entity;
        }
        catch (Exception pEx)
        {
            pUnitOfWork.DiscardChanges();
            throw ErrorMessageResolver.Resolve(pEx);
        }
    }
}
