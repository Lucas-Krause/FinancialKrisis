using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;
using FinancialKrisis.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinancialKrisis.Infrastructure.Repositories;

public class AccountRepository(FinancialKrisisDbContext pContext) : IAccountRepository
{
    public async Task AddAsync(Account pAccount)
    {
        pContext.Accounts.Add(pAccount);
        await pContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Account pAccount)
    {
        pContext.Accounts.Update(pAccount);
        await pContext.SaveChangesAsync();
    }

    public async Task<Account?> GetByIdAsync(Guid pId)
    {
        return await pContext.Accounts.FindAsync(pId);
    }

    public async Task<Account> GetByIdOrThrowAsync(Guid pId)
    {
        return await pContext.Accounts.FindAsync(pId) ?? throw new InvalidOperationException("Account not found.");
    }

    public async Task<IReadOnlyList<Account>> GetAllAsync()
    {
        return await pContext.Accounts
            .AsNoTracking()
            .ToListAsync();
    }
}
