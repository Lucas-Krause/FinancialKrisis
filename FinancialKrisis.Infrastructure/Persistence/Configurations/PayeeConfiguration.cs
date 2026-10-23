using FinancialKrisis.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialKrisis.Infrastructure.Persistence.Configurations;

public class PayeeConfiguration : IEntityTypeConfiguration<Payee>
{
    public void Configure(EntityTypeBuilder<Payee> pBuilder)
    {
        pBuilder.ToTable("Payees");

        pBuilder.HasKey(p => p.Id);

        pBuilder.HasEntityName(150);

        pBuilder
            .Property(p => p.IsActive)
            .IsRequired();

        pBuilder.HasIndex(p => p.Name.NormalizedValue).IsUnique();
        pBuilder.HasIndex(p => p.IsActive);
    }
}
