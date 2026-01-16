using FinancialKrisis.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinancialKrisis.Infrastructure.Persistence;

public class FinancialKrisisDbContext(DbContextOptions<FinancialKrisisDbContext> pOptions) : DbContext(pOptions)
{
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Payee> Payees => Set<Payee>();
    public DbSet<SubCategory> SubCategories => Set<SubCategory>();
}
