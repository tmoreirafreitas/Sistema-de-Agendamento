using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SA.Domain.Entities;

namespace SA.Infra.Data.Mapping
{
    public class ProcessoMap : IEntityTypeConfiguration<Processo>
    {
        public void Configure(EntityTypeBuilder<Processo> builder)
        {
            builder.HasKey(p => p.Id).HasName("ProcessoIdPk");
            builder.Property(p => p.Id).HasColumnName("ProcessoId");
            builder.Property(p => p.Numero)
                .HasColumnType("varchar(30)")
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(c => c.Estado)
                .HasColumnType("varchar(30)")
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(c => c.Valor)
                .HasColumnType("numeric(10,2)")                
                .IsRequired();

            builder.Property(c => c.DataCriacao)
                .HasColumnType("datetime")                
                .IsRequired();

            builder.Property(p => p.IsAtivo);
        }
    }
}
