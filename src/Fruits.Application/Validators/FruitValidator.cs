using FluentValidation;
using Fruits.Application.ViewModels;

namespace Fruits.Application.Validators
{
    public class FruitValidator : AbstractValidator<FruitViewModel>
    {
        public FruitValidator()
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