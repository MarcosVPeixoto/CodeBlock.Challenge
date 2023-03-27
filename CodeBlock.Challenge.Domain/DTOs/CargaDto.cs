using CodeBlock.Challenge.Domain.Enums;

namespace CodeBlock.Challenge.Domain.DTOs
{
    public class CargaDto
    {
        public Mineral Mineral{ get; set; }
        public DateTime DataRecebimento { get; set; }
        public decimal Valor { get; set; }
    }
}
