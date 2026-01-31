namespace FinancialKrisis.Domain.Interfaces;

public interface IFinancialMovementRepository<TMovement> : IGenericRepository<TMovement>, IDeletableRepository<TMovement>
    where TMovement : IEntity, IDeletable
{
}
