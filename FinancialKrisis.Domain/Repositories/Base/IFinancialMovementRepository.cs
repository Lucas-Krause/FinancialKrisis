using FinancialKrisis.Domain.Abstractions;

namespace FinancialKrisis.Domain.Repositories;

public interface IFinancialMovementRepository<TMovement> : IGenericRepository<TMovement>, IDeletableRepository<TMovement>
    where TMovement : IEntity, IDeletable
{
}
