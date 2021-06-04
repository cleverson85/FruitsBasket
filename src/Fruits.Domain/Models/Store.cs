namespace Fruits.Domain.Models
{
    public class Store : BaseEntity
    {
        public Fruit Fruit { get; set; }
        public int FruitId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalValue { get; set; }
    }
}
