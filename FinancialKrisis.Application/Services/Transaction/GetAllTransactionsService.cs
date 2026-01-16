using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class GetAllTransactionsService(ITransactionRepository pTransactionRepository)
{
    public async Task<IReadOnlyList<Transaction>> ExecuteAsync()
    {
        return await pTransactionRepository.GetAllAsync();
    }
}
