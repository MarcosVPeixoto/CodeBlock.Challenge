using CodeBlock.Challenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeBlock.Challenge.Repository.Configuration
{
    public class OperacaoCargueiroEntityTypeConfiguration : IEntityTypeConfiguration<OperacaoCargueiro>
    {
        public void Configure(EntityTypeBuilder<OperacaoCargueiro> builder)
        {
            builder.ToTable("operacaocargueiro");

            builder.HasKey(t => t.Id);

            builder.Property(x => x.Id)
                .HasColumnName("operacaoid");

            builder.Property(x => x.DataCriacao)
                .HasColumnName("datacriacao");

            builder.Property(x => x.DataEntrada)
                .HasColumnName("dataentrada");

            builder.Property(x => x.DataSaida)
                .HasColumnName("datasaida");

            builder.Property(x => x.ClasseCargueiro)
                .HasColumnName("classecargueiro");

            builder.Property(x => x.CargaOcupada)
                .HasColumnName("cargaocupada")
                .HasColumnType("decimal")
                .HasPrecision(4);
        }

        
    }
}
