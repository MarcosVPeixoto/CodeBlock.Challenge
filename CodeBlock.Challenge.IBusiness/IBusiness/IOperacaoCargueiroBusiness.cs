using CodeBlock.Challenge.Domain.Responses;
using CodeBlock.Challenge.Domain.DTOs;
using PagedList;

namespace CodeBlock.Challenge.IBusiness.IBusiness
{
    public interface IOperacaoCargueiroBusiness
    {
        Task<OperacaoCargueiroResponse> AddOperacao(OperacaoCargueiroDto operacao);
        GetCargasResponse GetCargasFiltradas(int mes, int ano, int pagina);
    }
}
