using CodeBlock.Challenge.Domain.DTOs;
using FluentValidation;
using System.Security.Cryptography.X509Certificates;

namespace CodeBlock.Challenge.Business.Validator
{
    public class OperacaoCargueiroDtoValidator : AbstractValidator<OperacaoCargueiroDto>
    {
        public OperacaoCargueiroDtoValidator()
        {
            RuleFor(x => x.DataEntrada)
                .NotEmpty()
                .NotNull()
                .WithMessage("Data não pode ser vazia");

            RuleFor(x => x.DataEntrada.DayOfWeek)
                .NotEqual(DayOfWeek.Sunday)
                .WithMessage("Não são permitadas operações aos domingos");

            RuleFor(x => x.DataEntrada.Hour)
                .GreaterThanOrEqualTo(8)                
                .WithMessage("Não são permitidas operações de saída antes das 8 AM");

            RuleFor(x => x.DataEntrada.AddHours(10))
                .GreaterThan(x => x.DataSaida)
                .WithMessage(x => $"A data de entrada deve ter pelo menos 10 horas de diferença da data de saída{x.DataSaida} // {x.DataEntrada}")
                .OverridePropertyName(nameof(OperacaoCargueiroDto.DataEntrada));

            RuleFor(x => x.DataSaida)
                .NotEmpty()
                .NotNull()
                .WithMessage("Data não pode ser vazia");

            RuleFor(x => x.DataSaida.DayOfWeek)
                .NotEqual(DayOfWeek.Sunday)
                .WithMessage("Não são permitadas operações aos domingos");

            RuleFor(x => x.ClasseCargueiro)
                .NotNull()
                .NotEmpty()
                .WithMessage("Classe não pode ser vazia");            
        }
    }
}
