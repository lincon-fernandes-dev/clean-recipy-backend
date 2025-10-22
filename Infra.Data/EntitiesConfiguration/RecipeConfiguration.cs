using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.EntitiesConfiguration
{
    public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            // Tabela
            builder.ToTable("Recipes");

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

            builder.Property(x => x.Description)
                .HasMaxLength(524)
                .IsRequired()
                .HasColumnName("Description");

            builder.Property(x => x.Instructions)
                .HasMaxLength(600)
                .IsRequired()
                .HasColumnName("Instructions");

            builder.Property(x => x.UserId)
                .IsRequired()
                .HasColumnName("UserId");

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
            builder.HasIndex(x => x.Name)
                .HasDatabaseName("IX_Recipes_Name");

            builder.HasIndex(x => x.UserId)
                .HasDatabaseName("IX_Recipes_UserId");

            // Relacionamentos
            builder.HasOne(x => x.User)
                .WithMany(x => x.Recipes)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasMany(x => x.Ingredients)
                .WithOne(x => x.Recipe)
                .HasForeignKey(x => x.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Votes)
                .WithOne(x => x.Recipe)
                .HasForeignKey(x => x.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}