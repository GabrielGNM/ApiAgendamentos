using Domain.Models.Controller.Requests;
using FluentValidation;
using System.Globalization;

namespace Domain.Models.Controller.Validators
{
    public class AgendamentoValidator : AbstractValidator<AgendamentoDto>
    {
        public AgendamentoValidator()
        {
            RuleFor(a => a.NomePaciente)
                .NotEmpty().WithMessage("O nome do paciente é obrigatório.")
                .MaximumLength(100).WithMessage("O nome do paciente deve ter no máximo 100 caracteres.");

            RuleFor(a => a.DataAtendimento)
                .NotNull()
                .WithMessage("objeto Nulo")

                .Must(x => DateTime.TryParseExact(x, "dd-MM-yyyy-HH-mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dataAtendimentoFormatada))
                .WithMessage("Formato Incorreto")

                .Must(x => ValidateDataAtendimento(x))
                .WithMessage("Valor menor que a Data Atual");

            RuleFor(a => a.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("O e-mail deve ser válido.");
        }

        private static bool ValidateDataAtendimento(string? dataAtendimentoStr)
        {

            if (!DateTime.TryParseExact(
                dataAtendimentoStr,
                "dd-MM-yyyy-HH-mm",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime dataAtendimentoFormatada))
            {
                return new DateTime() > DateTime.UtcNow.AddHours(-3);
            }

            return dataAtendimentoFormatada > DateTime.UtcNow.AddHours(-3);
        }
    }
}
