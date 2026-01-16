using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class GetAllPayeesService(IPayeeRepository pRepository)
{
    public async Task<IReadOnlyList<Payee>> ExecuteAsync()
    {
        return await pRepository.GetAllAsync();
    }
}
