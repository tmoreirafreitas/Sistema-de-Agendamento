using FluentValidation;
using SA.Domain.Entities;
using System;

namespace SA.Domain.Validators
{
    public class ProcessoValidator : AbstractValidator<Processo>
    {
        public ProcessoValidator()
        {
            RuleFor(p => p).NotNull().OnAnyFailure(p =>
            {
                throw new ArgumentNullException("O objeto cliente não pode ser nulo.");
            });

            RuleFor(p => p.Estado)
                .NotEmpty()
                .NotNull().WithMessage("É necessário informar o Estado");

            RuleFor(p => p.Numero)
                .NotEmpty()
                .NotNull().WithMessage("É necessário informar o Número");

            RuleFor(p => p.DataCriacao)                
                .NotNull().WithMessage("É necessário informar o Estado");

            RuleFor(p => p.Valor)
                .NotEmpty()
                .NotNull().WithMessage("É necessário informar o Valor");
        }
    }
}
