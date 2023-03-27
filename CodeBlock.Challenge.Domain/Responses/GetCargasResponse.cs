using CodeBlock.Challenge.Domain.DTOs;
using PagedList;

namespace CodeBlock.Challenge.Domain.Responses
{
    public class GetCargasResponse
    {
        public IPagedList<CargaDto> CargasDto { get; set; }
        public int TotalItemCount { get; set; }
    }
}
