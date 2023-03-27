using CodeBlock.Challenge.Domain.Enums;

namespace CodeBlock.Challenge.Domain.DTOs
{
    public class OperacaoCargueiroDto
    {
        public DateTime DataEntrada { get; set; }
        public DateTime DataSaida{ get; set; }
        public ClasseCargueiro ClasseCargueiro { get; set; }        
        public int GetQuantidadeMaximaCargueirosClasse() => ClasseCargueiro switch
        {
            ClasseCargueiro.I => 15,
            ClasseCargueiro.II => 10,
            ClasseCargueiro.III => 3,
            ClasseCargueiro.IV => 2,
            _ => throw new ArgumentOutOfRangeException(nameof(ClasseCargueiro), "Classe do cargueiro inválida"),

        };
    }
}
