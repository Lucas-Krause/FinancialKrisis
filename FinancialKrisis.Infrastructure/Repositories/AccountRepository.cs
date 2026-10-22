using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;
using FinancialKrisis.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinancialKrisis.Infrastructure.Repositories;

public class AccountRepository(FinancialKrisisDbContext pContext) : BaseRepository<Account>(pContext), IAccountRepository
{
    public async Task<bool> ExistsByNameAsync(string pName, Guid? pIgnoredId = null)
    {
        string normalizedName = pName.Trim().ToLower();
        return await _dbSet.AnyAsync(a => a.Name.ToLower() == normalizedName && (pIgnoredId == null || a.Id != pIgnoredId.Value));
    }
}
