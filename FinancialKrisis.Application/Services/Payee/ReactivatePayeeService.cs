using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public class ReactivatePayeeService(IPayeeRepository pPayeeRepository, IUnitOfWork pUnitOfWork) : ReactivateEntityService<Payee, IPayeeRepository>(pPayeeRepository, pUnitOfWork)
{
}
