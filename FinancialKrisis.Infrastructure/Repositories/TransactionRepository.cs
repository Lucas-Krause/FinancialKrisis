using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;
using FinancialKrisis.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinancialKrisis.Infrastructure.Repositories;

public class TransactionRepository(FinancialKrisisDbContext pContext) : ITransactionRepository
{
    public async Task AddAsync(Transaction pTransaction)
    {
        pContext.Transactions.Add(pTransaction);
        await pContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Transaction pTransaction)
    {
        pContext.Transactions.Update(pTransaction);
        await pContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid pId)
    {
        Transaction? transaction = await pContext.Transactions.FindAsync(pId);
        if (transaction is not null)
        {
            pContext.Transactions.Remove(transaction);
            await pContext.SaveChangesAsync();
        }
    }

    public async Task<Transaction?> GetByIdAsync(Guid pId)
    {
        return await pContext.Transactions
            .Include(t => t.Account)
            .Include(t => t.Payee)
            .Include(t => t.Category)
            .Include(t => t.SubCategory)
            .FirstOrDefaultAsync(t => t.Id == pId);
    }

    public async Task<Transaction> GetByIdOrThrowAsync(Guid pId)
    {
        return await GetByIdAsync(pId) ?? throw new InvalidOperationException("Transaction not found.");
    }

    public async Task<IReadOnlyList<Transaction>> GetAllAsync()
    {
        return await pContext.Transactions
            .Include(t => t.Account)
            .Include(t => t.Payee)
            .Include(t => t.Category)
            .Include(t => t.SubCategory)
            .AsNoTracking()
            .ToListAsync();
    }
}
