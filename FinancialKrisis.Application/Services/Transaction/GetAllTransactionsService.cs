using FinancialKrisis.Application.Helpers;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class GetAllTransactionsService(ITransactionRepository pTransactionRepository)
{
    public async Task<IReadOnlyList<Transaction>> ExecuteAsync()
    {
        try
        {
            return await pTransactionRepository.GetAllAsync();
        }
        catch (Exception pEx)
        {
            throw ErrorMessageResolver.Resolve(pEx);
        }
    }
}
