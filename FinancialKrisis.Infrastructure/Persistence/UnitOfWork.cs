using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Infrastructure.Persistence;

public class UnitOfWork(FinancialKrisisDbContext pContext) : IUnitOfWork
{
    public async Task SaveChangesAsync()
    {
        await pContext.SaveChangesAsync();
    }

    public void DiscardChanges()
    {
        pContext.ChangeTracker.Clear();
    }
}
