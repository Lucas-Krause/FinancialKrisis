using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;
using FinancialKrisis.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinancialKrisis.Infrastructure.Repositories;

public class TransactionRepository(FinancialKrisisDbContext pContext) : BaseRepository<Transaction>(pContext), ITransactionRepository
{
    public override async Task<Transaction?> GetByIdAsync(Guid pId)
    {
        return await _dbSet
            .Include(t => t.Account)
            .Include(t => t.Payee)
            .Include(t => t.Category)
            .Include(t => t.SubCategory)
            .FirstOrDefaultAsync(t => t.Id == pId);
    }

    public override async Task<IReadOnlyList<Transaction>> GetAllAsync()
    {
        return await _dbSet 
            .Include(t => t.Account)
            .Include(t => t.Payee)
            .Include(t => t.Category)
            .Include(t => t.SubCategory)
            .AsNoTracking()
            .ToListAsync();
    }
}
