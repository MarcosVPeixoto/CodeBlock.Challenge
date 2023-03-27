using CodeBlock.Challenge.Domain.DTOs;
using CodeBlock.Challenge.Domain.Entities;
using CodeBlock.Challenge.Domain.Enums;
using CodeBlock.Challenge.Domain.Responses;
using CodeBlock.Challenge.IBusiness.IBusiness;
using CodeBlock.Challenge.IRepository.Repositories;
using FluentValidation;
using System.Net;

namespace CodeBlock.Challenge.Business.Business
{
    public class OperacaoCargueiroBusiness : IOperacaoCargueiroBusiness
    {
        private readonly IOperacaoCargueiroRepository _operacaoRepository;
        private readonly ICargaRepository _cargaRepository;
        private readonly ICargaSemanalBusiness _cargaBusiness;
        private readonly IValidator<OperacaoCargueiroDto> _validator;
        public OperacaoCargueiroBusiness(IOperacaoCargueiroRepository operacaoRepository, ICargaRepository cargaRepository, ICargaSemanalBusiness cargaBusiness, IValidator<OperacaoCargueiroDto> validator)
        {
            _operacaoRepository = operacaoRepository;
            _cargaRepository = cargaRepository;
            _cargaBusiness = cargaBusiness;
            _validator = validator;
        }

        
        public async Task<OperacaoCargueiroResponse> AddOperacao(OperacaoCargueiroDto dto)
        {
            // validar dto
            var result = _validator.Validate(dto);
            if (!result.IsValid) 
            {
                return new OperacaoCargueiroResponse (HttpStatusCode.BadRequest, result );
            }
            ValidarDisponibilidadeCargueiros(dto);
            var operacaoCargueiro = new OperacaoCargueiro(dto.DataEntrada, dto.DataSaida, dto.ClasseCargueiro);
            var horaPartidaPlaneta = dto.DataEntrada.AddHours(-5);
            var cargaRestante = GetCargasRestantesSemana(horaPartidaPlaneta);            
            List<Carga> cargas = GetCargas(operacaoCargueiro, cargaRestante);            
            _operacaoRepository.Add(operacaoCargueiro);
            cargas.ForEach(x => _cargaRepository.Add(x));
            await _operacaoRepository.SaveContext();
            return new OperacaoCargueiroResponse (HttpStatusCode.Created, null);
        }

        private void ValidarDisponibilidadeCargueiros(OperacaoCargueiroDto dto)
        {
            var cargueirosViajando = _operacaoRepository.GetCargueirosViajandoMesmaClasse(dto);
            var maximoCargueirosClasse = dto.GetQuantidadeMaximaCargueirosClasse();
            if (cargueirosViajando >= maximoCargueirosClasse) throw new Exception("Limite de cargueiros viajando da classe informada atingida");
        }

        private List<Carga> GetCargas(OperacaoCargueiro operacaoCargueiro, CargaSemanalDto cargaRestante)
        {
            if (operacaoCargueiro.ClasseCargueiro == ClasseCargueiro.IV)
            {
                return GetCargasCargueiroIV(operacaoCargueiro, cargaRestante);
            }
            else
            {
                var cargas = new List<Carga>();
                var carga = GetCargaIndividual(operacaoCargueiro, cargaRestante, operacaoCargueiro.GetMineralTransportado());
                if (carga != null) cargas.Add(carga);
                return cargas;
            }
        }

        private Carga GetCargaIndividual(OperacaoCargueiro operacaoCargueiro, CargaSemanalDto cargaRestante, Mineral mineral)
        {
            var capacidadeMaximaCagueiro = operacaoCargueiro.GetCapacidadeMaximaCargueirosClasse();
            var capacidadeRestanteCargueiro = capacidadeMaximaCagueiro - operacaoCargueiro.CargaOcupada;
            var cargaRestanteMineral = cargaRestante.GetPesoMineral(mineral);
            var temMineralDisponivel = cargaRestanteMineral > 0;
            var cargueiroPodeCarregar = capacidadeRestanteCargueiro > 0;
            var restanteExcedeCapacidadeCargueiro = cargaRestanteMineral > capacidadeRestanteCargueiro;
            if (temMineralDisponivel && restanteExcedeCapacidadeCargueiro && cargueiroPodeCarregar)
            {
                operacaoCargueiro.CargaOcupada = capacidadeRestanteCargueiro;
                return new Carga(mineral, operacaoCargueiro.DataEntrada, operacaoCargueiro.ClasseCargueiro, capacidadeRestanteCargueiro);
            }
            else if (temMineralDisponivel && cargueiroPodeCarregar)
            {
                operacaoCargueiro.CargaOcupada += cargaRestanteMineral;
                return new Carga(mineral, operacaoCargueiro.DataEntrada, operacaoCargueiro.ClasseCargueiro, cargaRestanteMineral);                
            }
            return null;
        }

        private List<Carga> GetCargasCargueiroIV(OperacaoCargueiro operacao, CargaSemanalDto cargaRestante)
        {
            var cargas = new List<Carga>();
            var cargaB = GetCargaIndividual(operacao, cargaRestante, Mineral.RiscoBiologico);
            var cargaC = GetCargaIndividual(operacao, cargaRestante, Mineral.Refrigerado);
            if (cargaB != null) cargas.Add(cargaB);
            if (cargaC != null) cargas.Add(cargaC);
            return cargas;
        }

        private CargaSemanalDto GetCargasRestantesSemana(DateTime dataSaidaPlaneta)
        {
            var cargasDisponivelSemanal = _cargaBusiness.GetCargaDisponivelSemanal(dataSaidaPlaneta);
            var cargaExtraida = GetCargaExtraidaSemana(dataSaidaPlaneta);
            return new CargaSemanalDto
            {
                A = cargasDisponivelSemanal.A - cargaExtraida.A,
                B = cargasDisponivelSemanal.B - cargaExtraida.B,
                C = cargasDisponivelSemanal.C - cargaExtraida.C,
                D = cargasDisponivelSemanal.D - cargaExtraida.D
            };
        }

        private CargaSemanalDto GetCargaExtraidaSemana(DateTime data)
        {
            var semana1 = data.Day >= 1 && data.Day <= 7;
            var semana2 = data.Day >= 8 && data.Day <= 14;
            var semana3 = data.Day >= 15 && data.Day <= 21;
            DateTime dataInicio;
            DateTime dataFim;
            if (semana1)
            {
                dataInicio = new DateTime(data.Year, data.Month, 1);
                dataFim = new DateTime(data.Year, data.Month, 7);
            }
            else if (semana2)
            {
                dataInicio = new DateTime(data.Year, data.Month, 8);
                dataFim = new DateTime(data.Year, data.Month, 14);
            }
            else if (semana3)
            {
                dataInicio = new DateTime(data.Year, data.Month, 15);
                dataFim = new DateTime(data.Year, data.Month, 22);
            }
            else
            {
                var ultimoDiaMes = GetUltimoDiaMes(data);
                dataInicio = new DateTime(data.Year, data.Month, 1);
                dataFim = new DateTime(data.Year, data.Month, ultimoDiaMes);
            }
            var a = _cargaRepository.GetCargaExtraida(dataInicio, dataFim, Mineral.Inflamavel);
            var b = _cargaRepository.GetCargaExtraida(dataInicio, dataFim, Mineral.RiscoBiologico);
            var c = _cargaRepository.GetCargaExtraida(dataInicio, dataFim, Mineral.Refrigerado);
            var d = _cargaRepository.GetCargaExtraida(dataInicio, dataFim, Mineral.SemCaracteristicas);
            return new CargaSemanalDto { A = a, B = b, C = c, D = d };

        }
        private static int GetUltimoDiaMes(DateTime data)
        {
            if (DateTime.IsLeapYear(data.Year) && data.Day > 22) return 29;
            return GetDiasMes(data.Day);
        }
        private static int GetDiasMes(int mes) => mes switch
        {
            1 => 31,
            2 => 28,
            3 => 31,
            4 => 30,
            5 => 31,
            6 => 30,
            7 => 31,
            8 => 31,
            9 => 30,
            10 => 31,
            11 => 30,
            _ => 31
        };

        public GetCargasResponse GetCargasFiltradas(int mes, int ano, int pagina)
        {
            if (pagina <= 0) pagina = 1;
            var itens = _cargaRepository.GetCargasFiltradas(mes, ano, pagina);
            return new GetCargasResponse { CargasDto = itens, TotalItemCount = itens.TotalItemCount};
        }
    }
}
