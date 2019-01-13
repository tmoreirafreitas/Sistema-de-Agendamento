using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SA.Domain.Entities;

namespace SA.Infra.Data.Mapping
{
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(c => c.Id).HasName("ClienteIdPk");
            builder.Property(c => c.Id).HasColumnName("ClienteId");
            builder.Property(c => c.Cnpj)
                .HasColumnType("varchar(18)")
                .HasMaxLength(18)
                .IsRequired();

            builder.Property(c => c.Estado)
                .HasColumnType("varchar(30)")
                .HasMaxLength(30)
                .IsRequired();
        }
    }
}
