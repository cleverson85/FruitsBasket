using Fruits.Application.Validators;

namespace Fruits.Application.ViewModels
{
    public class FruitViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int AvailableQuantity { get; set; }
        public byte[] Picture { get; set; }
        public decimal Price { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new FruitValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
