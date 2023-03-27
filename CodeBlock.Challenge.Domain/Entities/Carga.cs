using CodeBlock.Challenge.Domain.Enums;

namespace CodeBlock.Challenge.Domain.Entities
{
    public class Carga : BaseEntity
    {
        public decimal Valor { get; set; }
        public Mineral Mineral { get; set; }
        public DateTime DataRecebimento { get; set; }
        public ClasseCargueiro CargueiroUtilizado { get; set; }
        public decimal Peso { get; set; }       

        public Carga(Mineral mineral, DateTime dataRecebimento, ClasseCargueiro cargueiroUtilizado, decimal peso)
            : base()
        {            
            Mineral = mineral;
            DataRecebimento = dataRecebimento;
            CargueiroUtilizado = cargueiroUtilizado;
            Peso = peso;
            Valor = peso * CalcularValorMaterial();
        }       

        private decimal CalcularValorMaterial() => Mineral switch
        {
            Mineral.Inflamavel => 5000,
            Mineral.RiscoBiologico => 10000000000,
            Mineral.Refrigerado => 30000000,
            Mineral.SemCaracteristicas => 1000,
            _ => throw new ArgumentOutOfRangeException(nameof(Mineral), "Mineral invalido")
        };
    }
}
