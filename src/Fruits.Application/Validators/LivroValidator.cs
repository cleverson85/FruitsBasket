using FluentValidation;
using Fruits.Domain.Models;

namespace Fruits.Application.Validators
{
    public class LivroValidator : AbstractValidator<Fruit>
    {
        public LivroValidator()
        {
            RuleFor(c => c.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Informe o nome.");

            RuleFor(c => c.AvailableQuantity)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("Informe a quantidade disponível.");

            RuleFor(c => c.Price)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("Informe o valor.");

            RuleFor(c => c.Picture)
                .NotEmpty()
                .WithMessage("Informe a imagem.");
        }
    }
}