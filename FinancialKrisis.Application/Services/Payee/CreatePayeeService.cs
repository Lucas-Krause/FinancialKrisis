using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class CreatePayeeService(IPayeeRepository pRepository)
{
    public async Task<Payee> ExecuteAsync(CreatePayeeDTO pCreatePayeeDTO)
    {
        var payee = new Payee(pCreatePayeeDTO.Name);
        await pRepository.AddAsync(payee);
        return payee;
    }
}
