using Domain.Models.Controller.Requests;
using FluentValidation;

namespace Domain.Models.Controller.Validators
{
    public class AdicionarEmailMedicoValidator : AbstractValidator<AdicionarEmailMedicoDto>
    {
        public AdicionarEmailMedicoValidator()
        {
            RuleFor(a => a.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("O e-mail deve ser válido.");

            RuleFor(a => a.Id)
                .NotNull()
                .WithMessage("objeto Nulo")

                .NotEmpty()
                .WithMessage("objeto empty");
        }
    }
}
