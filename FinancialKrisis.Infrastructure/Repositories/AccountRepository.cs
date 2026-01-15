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

    public async Task<IReadOnlyList<Account>> GetAllAsync()
    {
        return await pContext.Accounts
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Account?> GetByIdAsync(Guid pId)
    {
        return await pContext.Accounts.FindAsync(pId);
    }

    public async Task<Account> GetByIdOrThrowAsync(Guid pId)
    {
        Account? account = await pContext.Accounts.FindAsync(pId);

        return account is null ? throw new InvalidOperationException("Account not found.") : account;
    }


    public async Task UpdateAsync(Account pAccount)
    {
        pContext.Accounts.Update(pAccount);
        await pContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid pId)
    {
        Account? account = await pContext.Accounts.FindAsync(pId);
        if (account is null) return;

        pContext.Accounts.Remove(account);
        await pContext.SaveChangesAsync();
    }
}
