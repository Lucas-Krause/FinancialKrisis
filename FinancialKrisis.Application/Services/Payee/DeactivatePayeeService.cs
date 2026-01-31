using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class DeactivatePayeeService(IPayeeRepository pPayeeRepository) : DeactivateEntityService<Payee, IPayeeRepository>(pPayeeRepository)
{
}
