using Domain.Entities;
using Domain.Enums;
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

            // Status
            builder.Property(x => x.Status)
                .IsRequired()
                .HasConversion<int>()
                .HasColumnName("Status")
                .HasDefaultValue(UserStatus.Active)
                .HasSentinel((UserStatus)999);

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
            builder.HasIndex(x => x.Email)
                .IsUnique()
                .HasDatabaseName("IX_Users_Email");

            builder.HasIndex(x => x.Name)
                .HasDatabaseName("IX_Users_Name");

            builder.HasIndex(x => x.Status)
                .HasDatabaseName("IX_Users_Status");

            // Relacionamentos
            builder.HasMany(x => x.Recipes)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Recipes)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .IsRequired(false) // ✅ Torna opcional
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasMany(x => x.Votes)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .IsRequired(false) // ✅ Torna opcional  
                .OnDelete(DeleteBehavior.ClientSetNull);

            // ✅ Query Filter para Soft Delete - Agora seguro!
            builder.HasQueryFilter(x => x.Status != UserStatus.Deleted);


           

        }
    }
}