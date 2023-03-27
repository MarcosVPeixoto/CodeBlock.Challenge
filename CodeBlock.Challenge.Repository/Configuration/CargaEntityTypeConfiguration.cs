using CodeBlock.Challenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeBlock.Challenge.Repository.Configuration
{
    public class CargaEntityTypeConfiguration : IEntityTypeConfiguration<Carga>
    {
        public void Configure(EntityTypeBuilder<Carga> builder)
        {
            builder.ToTable("carga");
            
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("cargaId");

            builder.Property(x => x.Peso)
                .HasColumnName("peso")
                .HasColumnType("decimal")
                .HasPrecision(4);
            
            builder.Property(x => x.Valor)
                .HasColumnName("valor")
                .HasColumnType("decimal")
                .HasPrecision(4);

            builder.Property(x => x.DataCriacao)
                .HasColumnName("datacriacao");

            builder.Property(x => x.DataRecebimento)
                .HasColumnName("datarecebimento");

            builder.Property(x => x.CargueiroUtilizado)
                .HasColumnName("classecargueiro");

            builder.Property(x => x.Mineral)
                .HasColumnName("mineral");
            
        }
    }
}
