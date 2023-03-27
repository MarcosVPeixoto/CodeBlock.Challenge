using CodeBlock.Challenge.Domain.Entities;

namespace CodeBlock.Challenge.IBusiness.IBusiness
{
    public interface ICargaSemanalBusiness
    {
        CargaSemanalDto GetCargaDisponivelSemanal(DateTime data);
    }
}
