using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.EntitiesConfiguration
{
    public class VoteConfiguration : IEntityTypeConfiguration<Vote>
    {
        public void Configure(EntityTypeBuilder<Vote> builder)
        {
            // Tabela
            builder.ToTable("Votes");

            // Chave Primária
            builder.HasKey(x => x.Id);

            // Propriedades
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.UserId)
                .IsRequired()
                .HasColumnName("UserId");

            builder.Property(x => x.RecipeId)
                .IsRequired()
                .HasColumnName("RecipeId");

            builder.Property(x => x.IsUpvote)
                .IsRequired()
                .HasColumnName("IsUpvote");

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
            builder.HasIndex(x => new { x.UserId, x.RecipeId })
                .IsUnique()
                .HasDatabaseName("IX_Votes_UserId_RecipeId");

            builder.HasIndex(x => x.UserId)
                .HasDatabaseName("IX_Votes_UserId");

            builder.HasIndex(x => x.RecipeId)
                .HasDatabaseName("IX_Votes_RecipeId");

            builder.HasIndex(x => x.IsUpvote)
                .HasDatabaseName("IX_Votes_IsUpvote");

            // Relacionamentos
            builder.HasOne(x => x.User)
                .WithMany(x => x.Votes)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Recipe)
                .WithMany(x => x.Votes)
                .HasForeignKey(x => x.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}