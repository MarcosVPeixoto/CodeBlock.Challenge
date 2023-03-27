
using CodeBlock.Challenge.Domain.DTOs;
using CodeBlock.Challenge.Domain.Responses;
using CodeBlock.Challenge.IBusiness.IBusiness;
using Microsoft.AspNetCore.Mvc;

namespace CodeBlock.Challenge.Api.Controllers
{
    [ApiController]
    [Route("operacaocargueiro")]
    public class OperacaoCargueiroController : ControllerBase
    {
        private readonly IOperacaoCargueiroBusiness _business;

        public OperacaoCargueiroController(IOperacaoCargueiroBusiness business)
        {
            _business = business;
        } 
        
        [HttpPost]
        public async Task<OperacaoCargueiroResponse>AddEntrada(OperacaoCargueiroDto dto)
        {
            var result = await _business.AddOperacao(dto);
            return result;
        }

        [HttpGet]
        [Route("getcargas/mes/{mes:int}/ano/{ano}/pagina/{pagina}")]
        public GetCargasResponse GetCargas([FromRoute]int mes, int ano, int pagina)
        {
            return _business.GetCargasFiltradas(mes, ano, pagina);
        }
    }
}
