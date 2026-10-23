using FinancialKrisis.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialKrisis.Infrastructure.Persistence.Configurations;

public static class EntityNameConfigurationExtensions
{
    extension<TEntity>(EntityTypeBuilder<TEntity> pBuilder) where TEntity : class, INamedEntity
    {
        public void HasEntityName(int pMaxLength)
        {
            pBuilder.ComplexProperty(p => p.Name, pName =>
            {
                pName
                    .Property(p => p.Value)
                    .HasColumnName("Name")
                    .IsRequired()
                    .HasMaxLength(pMaxLength);

                pName
                    .Property(p => p.NormalizedValue)
                    .HasColumnName("NormalizedName")
                    .IsRequired()
                    .HasMaxLength(pMaxLength);
            });
        }
    }
}
