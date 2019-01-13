using FluentValidation;
using SA.Domain.Entities;
using System;

namespace SA.Domain.Validators
{
    public class ClienteValidator : AbstractValidator<Cliente>
    {
        public ClienteValidator()
        {
            RuleFor(c => c).NotNull().OnAnyFailure(cl =>
            {
                throw new ArgumentNullException("O objeto cliente não pode ser nulo.");
            });

            RuleFor(c=>c.Cnpj)
                .NotEmpty()
                .NotNull().WithMessage("É necessário informar o CNPJ");

            RuleFor(c => c.Estado)
                .NotEmpty()
                .NotNull().WithMessage("É necessário informar o Estado");

            RuleFor(c => c.Nome)
                .NotEmpty()
                .NotNull().WithMessage("É necessário informar o Nome");
        }
    }
}
