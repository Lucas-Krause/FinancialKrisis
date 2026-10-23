using FinancialKrisis.Domain.Interfaces;
using FinancialKrisis.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialKrisis.Infrastructure.Persistence.Configurations;

public static class EntityNameConfigurationExtensions
{
    extension<TEntity>(EntityTypeBuilder<TEntity> pBuilder) where TEntity : class, INamedEntity
    {
        public void HasEntityName()
        {
            pBuilder.ComplexProperty(p => p.Name, pName =>
            {
                pName
                    .Property(p => p.Value)
                    .HasColumnName("Name")
                    .IsRequired()
                    .HasMaxLength(EntityName.MaxLength);

                pName
                    .Property(p => p.NormalizedValue)
                    .HasColumnName("NormalizedName")
                    .IsRequired()
                    .HasMaxLength(EntityName.MaxLength);
            });
        }
    }
}
