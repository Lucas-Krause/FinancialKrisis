using FinancialKrisis.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FinancialKrisis.Infrastructure.Persistence;

public sealed class AuditSaveChangesInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData pEventData, InterceptionResult<int> pResult, CancellationToken pCancellationToken = default)
    {
        DbContext? context = pEventData.Context;

        if (context is null)
            return base.SavingChangesAsync(pEventData, pResult, pCancellationToken);

        DateTime now = DateTime.Now;

        foreach (EntityEntry<Entity> entry in context.ChangeTracker.Entries<Entity>())
        {
            if (entry.State == EntityState.Added)
                entry.Entity.SetCreatedAt(now);

            if (entry.State == EntityState.Modified)
                entry.Entity.SetUpdatedAt(now);
        }

        return base.SavingChangesAsync(pEventData, pResult, pCancellationToken);
    }
}
