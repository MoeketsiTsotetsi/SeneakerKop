namespace SeneakerKop.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int SneakerId { get; set; }
        public string   ProductName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
