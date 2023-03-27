using CodeBlock.Challenge.Domain.Enums;

namespace CodeBlock.Challenge.Domain.Entities
{
    public class OperacaoCargueiro : BaseEntity
    {
        public DateTime DataEntrada { get; set; }
        public DateTime DataSaida { get; set; }
        public ClasseCargueiro ClasseCargueiro { get; set; }
        public decimal CargaOcupada { get; set; }

        public OperacaoCargueiro(DateTime dataEntrada, DateTime dataSaida, ClasseCargueiro classeCargueiro)
            : base()
        {
            DataEntrada = dataEntrada;
            DataSaida = dataSaida;
            ClasseCargueiro = classeCargueiro;
            CargaOcupada = 0;
        }       

        public decimal GetCapacidadeMaximaCargueirosClasse() => ClasseCargueiro switch
        {
            ClasseCargueiro.I => 5,
            ClasseCargueiro.II => 3,
            ClasseCargueiro.III => 2,
            ClasseCargueiro.IV => 0.5m,
            _ => throw new ArgumentOutOfRangeException(nameof(ClasseCargueiro), "Classe do cargueiro inválida"),
        };

        public Mineral GetMineralTransportado() => ClasseCargueiro switch
        {
            ClasseCargueiro.I => Mineral.SemCaracteristicas,
            ClasseCargueiro.II => Mineral.Inflamavel,
            ClasseCargueiro.III => Mineral.Refrigerado,
            _ => throw new ArgumentOutOfRangeException(nameof(ClasseCargueiro), "Classe do cargueiro inválida"),
        };
    }
}
