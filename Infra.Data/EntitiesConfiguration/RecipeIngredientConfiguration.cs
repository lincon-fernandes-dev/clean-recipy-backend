using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.EntitiesConfiguration
{
    public class RecipeIngredientConfiguration : IEntityTypeConfiguration<RecipeIngredient>
    {
        public void Configure(EntityTypeBuilder<RecipeIngredient> builder)
        {
            // Tabela
            builder.ToTable("RecipeIngredients");

            // Chave Primária
            builder.HasKey(x => x.Id);

            // Propriedades
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.RecipeId)
                .IsRequired()
                .HasColumnName("RecipeId");

            builder.Property(x => x.IngredientId)
                .IsRequired()
                .HasColumnName("IngredientId");

            builder.Property(x => x.Quantity)
                .HasColumnType("decimal(10,4)")
                .IsRequired()
                .HasColumnName("Quantity");

            builder.Property(x => x.UnidadeMedida)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (UnidadeMedidaEnum)Enum.Parse(typeof(UnidadeMedidaEnum), v))
                .HasColumnName("UnidadeMedida")
                .HasMaxLength(20);

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
                .HasColumnName("LastModifiedBy")
                .HasDefaultValue("System");

            // Índices
            builder.HasIndex(x => new { x.RecipeId, x.IngredientId })
                .IsUnique()
                .HasDatabaseName("IX_RecipeIngredients_RecipeId_IngredientId");

            builder.HasIndex(x => x.RecipeId)
                .HasDatabaseName("IX_RecipeIngredients_RecipeId");

            builder.HasIndex(x => x.IngredientId)
                .HasDatabaseName("IX_RecipeIngredients_IngredientId");
        }
    }
}