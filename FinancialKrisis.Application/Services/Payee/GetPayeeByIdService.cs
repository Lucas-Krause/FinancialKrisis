using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class GetPayeeByIdService(IPayeeRepository pRepository)
{
    public async Task<Payee?> ExecuteAsync(Guid pPayeeId)
    {
        return await pRepository.GetByIdAsync(pPayeeId);
    }
}
