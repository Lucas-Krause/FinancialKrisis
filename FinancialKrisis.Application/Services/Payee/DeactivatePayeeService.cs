using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public class DeactivatePayeeService(IPayeeRepository pPayeeRepository) : DeactivateEntityService<Payee, IPayeeRepository>(pPayeeRepository)
{
}
