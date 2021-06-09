namespace Fruits.Application.ViewModels
{
    public class StoreViewModel : BaseViewModel
    {
        public int FruitId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalValue { get; set; }

        public override bool IsValid()
        {
            return true;
        }
    }
}
