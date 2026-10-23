using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public class UpdateTransactionService(
    ITransactionRepository pTransactionRepository,
    IAccountRepository pAccountRepository,
    IPayeeRepository pPayeeRepository,
    ICategoryRepository pCategoryRepository,
    ISubcategoryRepository pSubcategoryRepository,
    IUnitOfWork pUnitOfWork)
    : UpdateFinancialMovementService<Transaction, ITransactionRepository, UpdateTransactionDTO>(
        pTransactionRepository,
        pAccountRepository,
        pCategoryRepository,
        pSubcategoryRepository,
        pPayeeRepository,
        pUnitOfWork)
{
    protected override void ApplyChangesToMovement(Transaction pTransaction, UpdateTransactionDTO pUpdateTransactionDTO)
    {
        if (pUpdateTransactionDTO.DateTime.IsDefined)
            pTransaction.ChangeDateTime(pUpdateTransactionDTO.DateTime.Value);
    }
}
