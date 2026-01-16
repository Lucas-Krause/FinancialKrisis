using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;
using FinancialKrisis.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinancialKrisis.Infrastructure.Repositories;

public class PayeeRepository(FinancialKrisisDbContext pContext) : IPayeeRepository
{
    public async Task AddAsync(Payee pPayee)
    {
        pContext.Payees.Add(pPayee);
        await pContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Payee pPayee)
    {
        pContext.Payees.Update(pPayee);
        await pContext.SaveChangesAsync();
    }

    public async Task<Payee?> GetByIdAsync(Guid pId)
    {
        return await pContext.Payees.FindAsync(pId);
    }

    public async Task<Payee> GetByIdOrThrowAsync(Guid pId)
    {
        return await pContext.Payees.FindAsync(pId) ?? throw new InvalidOperationException("Payee not found.");
    }

    public async Task<IReadOnlyList<Payee>> GetAllAsync()
    {
        return await pContext.Payees.AsNoTracking().ToListAsync();
    }
}
