using CodeBlock.Challenge.Domain.DTOs;
using CodeBlock.Challenge.Domain.Entities;
using CodeBlock.Challenge.Domain.Enums;
using CodeBlock.Challenge.IRepository.Repositories;
using CodeBlock.Challenge.Repository.Data;
using Microsoft.EntityFrameworkCore;
using PagedList;

namespace CodeBlock.Challenge.Repository.Repositories
{
    public class CargaRepository : Repository<Carga>, ICargaRepository
    {
        public CargaRepository(DataContext context) 
            : base(context)
        {
        }
        public decimal GetCargaExtraida (DateTime dataInicio, DateTime dataFim, Mineral mineral) 
        {
            return _context.Cargas
                        .Where(x => DateTime.Compare(x.DataRecebimento.Date, dataInicio.Date) >= 0 &&
                                    DateTime.Compare(x.DataRecebimento.Date, dataFim.Date) <= 0 &&
                                    x.Mineral == mineral)
                        .Select(x => x.Peso)
                        .Sum();
        }

        public IPagedList<CargaDto> GetCargasFiltradas(int mes, int ano, int pagina  )
        {
            return _context.Cargas
                .Where(x => x.DataRecebimento.Month == mes && x.DataRecebimento.Year == ano)                
                .Select(x => new CargaDto { DataRecebimento = x.DataRecebimento, Mineral = x.Mineral, Valor = x.Valor })
                .ToPagedList(pagina, 10);            
        }        
    }
}