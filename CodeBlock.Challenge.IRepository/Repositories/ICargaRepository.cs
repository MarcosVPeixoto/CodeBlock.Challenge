using CodeBlock.Challenge.Domain.DTOs;
using CodeBlock.Challenge.Domain.Entities;
using CodeBlock.Challenge.Domain.Enums;
using PagedList;

namespace CodeBlock.Challenge.IRepository.Repositories
{
    public interface ICargaRepository : IRepository<Carga>
    {
        decimal GetCargaExtraida(DateTime dataInicio, DateTime dataFim, Mineral mineral);
        IPagedList<CargaDto> GetCargasFiltradas(int mes, int ano, int pagina);
    }
}
