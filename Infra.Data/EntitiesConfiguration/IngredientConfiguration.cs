using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.EntitiesConfiguration
{
    public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            // Tabela
            builder.ToTable("Ingredients");

            // Chave Primária
            builder.HasKey(x => x.Id);

            // Propriedades
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired()
                .HasColumnName("Name");

            // Campos de Auditoria
            builder.Property(x => x.CreatedDate)
                .IsRequired()
                .HasColumnName("CreatedDate")
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(x => x.LastModifiedDate)
                .IsRequired()
                .HasColumnName("LastModifiedDate")
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(x => x.CreatedBy)
                .HasMaxLength(128)
                .IsRequired()
                .HasColumnName("CreatedBy");

            builder.Property(x => x.LastModifiedBy)
                .HasMaxLength(128)
                .IsRequired()
                .HasColumnName("ModifiedBy")
                .HasDefaultValue("System");
        }
    }
}