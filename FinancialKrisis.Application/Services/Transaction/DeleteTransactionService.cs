using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class DeleteTransactionService(ITransactionRepository pTransactionRepository)
{
    public async Task ExecuteAsync(Guid pTransactionId)
    {
        await pTransactionRepository.DeleteAsync(pTransactionId);
    }
}
