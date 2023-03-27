using CodeBlock.Challenge.Domain.Enums;

namespace CodeBlock.Challenge.Domain.Entities
{
    public class CargaSemanalDto
    {
        public decimal A { get; set; }
        public decimal B { get; set; }
        public decimal C { get; set; }
        public decimal D { get; set; }
        public decimal GetPesoMineral(Mineral mineral) => mineral switch
        {
            Mineral.Inflamavel => A,
            Mineral.RiscoBiologico => B,
            Mineral.Refrigerado => C,
            Mineral.SemCaracteristicas => D,
            _ => throw new ArgumentOutOfRangeException("Valor inválido")
        };       
    }
}
