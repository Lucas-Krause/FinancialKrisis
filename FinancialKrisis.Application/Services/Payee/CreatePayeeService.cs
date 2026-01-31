using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class CreatePayeeService(IPayeeRepository pPayeeRepository) : CreateEntityService<Payee, IPayeeRepository, CreatePayeeDTO>(pPayeeRepository)
{
    protected override async Task<Payee> CreateEntity(CreatePayeeDTO pCreateDTO)
    {
        return new Payee(pCreateDTO.Name);
    }
}
