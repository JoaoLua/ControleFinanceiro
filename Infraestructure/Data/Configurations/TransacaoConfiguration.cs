using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Data.Configurations
{
    public class TransacaoConfiguration: IEntityTypeConfiguration<Transacao>
    {
        public void Configure(EntityTypeBuilder<Transacao> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Valor).HasColumnType("decimal(18,2)");
            builder.Property(t => t.Data).IsRequired();
            builder.Property(t => t.Descricao).HasMaxLength(255);

            builder.HasOne(t => t.Conta)
                   .WithMany(c => c.Transacoes)
                   .HasForeignKey(t => t.ContaId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.Categoria)
                   .WithMany()
                   .HasForeignKey(t => t.CategoriaId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.User)
                   .WithMany()
                   .HasForeignKey(t => t.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
