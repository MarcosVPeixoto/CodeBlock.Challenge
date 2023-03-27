using CodeBlock.Challenge.Domain.DTOs;
using CodeBlock.Challenge.Domain.Entities;

namespace CodeBlock.Challenge.IRepository.Repositories
{
    public interface IOperacaoCargueiroRepository : IRepository<OperacaoCargueiro>
    {
        int GetCargueirosViajandoMesmaClasse(OperacaoCargueiroDto dto);
    }
}
