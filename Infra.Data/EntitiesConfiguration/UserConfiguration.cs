using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.EntitiesConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Tabela
            builder.ToTable("Users");

            // Chave Primária
            builder.HasKey(x => x.Id);

            // Propriedades
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(128)
                .IsRequired()
                .HasColumnName("Name");

            builder.Property(x => x.Email)
                .HasMaxLength(256)
                .IsRequired()
                .HasColumnName("Email");

            builder.Property(x => x.PasswordHash)
                .HasMaxLength(512)
                .IsRequired()
                .HasColumnName("PasswordHash");

            // Campos de Auditoria
            builder.Property(x => x.CreatedDate)
                .IsRequired()
                .HasColumnName("CreatedDate")
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(x => x.LastModifiedDate)
                .IsRequired()
                .HasColumnName("ModifiedDate")
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

            // Índices
            builder.HasIndex(x => x.Email)
                .IsUnique()
                .HasDatabaseName("IX_Users_Email");

            builder.HasIndex(x => x.Name)
                .HasDatabaseName("IX_Users_Name");

            // Relacionamentos
            builder.HasMany(x => x.Recipes)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Votes)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configurações adicionais
            builder.HasQueryFilter(x => !x.CreatedBy.Contains("DELETED")); // Exemplo de soft delete

            // Dados padrão (opcional - para desenvolvimento)
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                builder.HasData(
                    new User(1, "Administrador Sistema", "admin@recipes.com", "hashed_password", "system"),
                    new User(2, "Chef João Silva", "chef.joao@recipes.com", "hashed_password", "system")
                );
            }
        }
    }
}