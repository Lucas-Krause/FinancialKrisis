using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class UpdateTransactionService(
    ITransactionRepository pTransactionRepository,
    IAccountRepository pAccountRepository,
    IPayeeRepository pPayeeRepository,
    ICategoryRepository pCategoryRepository,
    ISubcategoryRepository pSubcategoryRepository)
    : UpdateFinancialMovementService<Transaction, ITransactionRepository, UpdateTransactionDTO>(
        pTransactionRepository,
        pAccountRepository,
        pCategoryRepository,
        pSubcategoryRepository,
        pPayeeRepository)
{
    protected override void ApplyChangesToMovement(Transaction pTransaction, UpdateTransactionDTO pUpdateTransactionDTO)
    {
        if (pUpdateTransactionDTO.DateTime.IsDefined)
            pTransaction.ChangeDateTime(pUpdateTransactionDTO.DateTime.Value);
    }
}
