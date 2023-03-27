using System.ComponentModel;

namespace CodeBlock.Challenge.Domain.Enums
{
    public enum Mineral
    {
        [Description("Inflamável")]
        Inflamavel = 0,
        [Description("Risco biológico")]
        RiscoBiologico = 1,
        [Description("Refrigerado")]
        Refrigerado = 2,
        [Description("Sem características especiais")]
        SemCaracteristicas = 3
    }
}
