using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public class CreateTransactionService(
    ITransactionRepository pTransactionRepository,
    IAccountRepository pAccountRepository,
    ICategoryRepository pCategoryRepository,
    ISubcategoryRepository pSubcategoryRepository,
    IPayeeRepository pPayeeRepository,
    IUnitOfWork pUnitOfWork)
    : CreateFinancialMovementService<Transaction, ITransactionRepository, CreateTransactionDTO>(
        pTransactionRepository,
        pAccountRepository,
        pCategoryRepository,
        pSubcategoryRepository,
        pPayeeRepository,
        pUnitOfWork)
{
    protected override Transaction CreateMovement(CreateTransactionDTO pDTO, Account pAccount, Payee? pPayee, Category? pCategory, Subcategory? pSubcategory)
    {
        return new(
                pAccount,
                pDTO.Amount,
                pDTO.DateTime,
                pDTO.Direction,
                pDTO.Memo,
                pDTO.Identifier,
                pPayee,
                pCategory,
                pSubcategory);
    }
}