using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class DeactivatePayeeService(IPayeeRepository pRepository)
{
    public async Task ExecuteAsync(Guid pPayeeId)
    {
        Payee payee = await pRepository.GetByIdOrThrowAsync(pPayeeId);
        payee.Deactivate();
        await pRepository.UpdateAsync(payee);
    }
}
