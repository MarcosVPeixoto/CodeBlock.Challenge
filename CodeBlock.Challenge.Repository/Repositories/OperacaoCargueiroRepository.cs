using CodeBlock.Challenge.Domain.DTOs;
using CodeBlock.Challenge.Domain.Entities;
using CodeBlock.Challenge.IRepository.Repositories;
using CodeBlock.Challenge.Repository.Data;
namespace CodeBlock.Challenge.Repository.Repositories
{
    public class OperacaoCargueiroRepository : Repository<OperacaoCargueiro>, IOperacaoCargueiroRepository
    {
        
        public OperacaoCargueiroRepository(DataContext context)
            : base (context)
        {
            
        } 
        public int GetCargueirosViajandoMesmaClasse(OperacaoCargueiroDto dto)
        {
            return _context.OperacaoCargueiro.Count(x => x.ClasseCargueiro == dto.ClasseCargueiro &&
                                                         DateTime.Compare(x.DataSaida, dto.DataSaida) <= 0 && 
                                                         DateTime.Compare(x.DataEntrada, dto.DataSaida) >= 0);
        }
    }
}
