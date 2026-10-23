namespace FinancialKrisis.Domain.Interfaces;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
    void DiscardChanges();
}
