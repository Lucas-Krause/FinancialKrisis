using FinancialKrisis.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinancialKrisis.Infrastructure.Persistence;

public class FinancialKrisisDbContext(DbContextOptions<FinancialKrisisDbContext> pOptions) : DbContext(pOptions)
{
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Payee> Payees => Set<Payee>();
    public DbSet<Subcategory> SubCategories => Set<Subcategory>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<PlannedTransaction> PlannedTransactions => Set<PlannedTransaction>();

    protected override void OnModelCreating(ModelBuilder pModelBuilder)
    {
        pModelBuilder.Entity<PlannedTransaction>(pEntity =>
        {
            pEntity.OwnsOne(p => p.Schedule);
        });

        pModelBuilder.ApplyConfigurationsFromAssembly(typeof(FinancialKrisisDbContext).Assembly);
        base.OnModelCreating(pModelBuilder);
    }
}
