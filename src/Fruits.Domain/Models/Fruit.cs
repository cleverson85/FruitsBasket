namespace Fruits.Domain.Models
{
    public class Fruit : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int AvailableQuantity { get; set; }
        public byte[] Picture { get; set; }
        public decimal Price { get; set; }
    }
}
