using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.EntitiesConfiguration
{
    public class InstructionConfiguration : IEntityTypeConfiguration<Instruction>
    {
        public void Configure(EntityTypeBuilder<Instruction> builder)
        {
            builder.ToTable("Instructions");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("IdInstruction")
                .IsRequired();

            builder.Property(i => i.Content)
                .IsRequired()
                .HasMaxLength(1000)
                .HasColumnName("Content");

            builder.Property(i => i.IdRecipe)
                .IsRequired()
                .HasColumnName("IdRecipe");

            // Campos de auditoria
            builder.Property(i => i.CreatedAt)
                .IsRequired()
                .HasColumnName("CreatedAt")
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(i => i.UpdatedAt)
                .IsRequired()
                .HasColumnName("UpdatedAt")
                .HasDefaultValueSql("GETUTCDATE()");

            // Relationships
            builder.HasOne(i => i.Recipe)
                .WithMany(r => r.Instructions)
                .HasForeignKey(i => i.IdRecipe)
                .OnDelete(DeleteBehavior.Cascade);

            // Índices
            builder.HasIndex(i => i.IdRecipe);
        }
    }
}
