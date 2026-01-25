using FinancialKrisis.Application.Helpers;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class GetAllPayeesService(IPayeeRepository pRepository)
{
    public async Task<IReadOnlyList<Payee>> ExecuteAsync()
    {
        try
        {
            return await pRepository.GetAllAsync();
        }
        catch (Exception pEx)
        {
            throw ErrorMessageResolver.Resolve(pEx);
        }
    }
}
